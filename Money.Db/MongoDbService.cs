using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.Extensions.Options;
using Money.Core;
using Money.Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Money.Db
{
  public class MongoDbService : IDbService
  {
    private readonly Lazy<IMongoCollection<Expense>> _expenses;
    private readonly Lazy<IMongoCollection<Filter>> _filters;

    public MongoDbService(IOptions<Options> options)
    {
      _expenses = new Lazy<IMongoCollection<Expense>>(() =>
        new MongoClient(options.Value.MoneyDbConnectionString)
          .GetDatabase("money")
          .GetCollection<Expense>("expenses"));

      _filters = new Lazy<IMongoCollection<Filter>>(() =>
        new MongoClient(options.Value.MoneyDbConnectionString)
          .GetDatabase("money")
          .GetCollection<Filter>("filters"));
    }

    public ICollection<Expense> GetExpenses()
    {
      return _expenses.Value.Find(new BsonDocument()).ToList();
    }

    public ICollection<Expense> GetFilteredExpenses(IEnumerable<string> filters, string month)
    {
      return _expenses.Value.FindSync(GetFilterDefinition(filters.ToList(), month)).ToList();
    }

    public void AddExpenses(IEnumerable<Expense> expenses)
    {
      _expenses.Value.InsertMany(expenses);
    }

    public void AddExpense(Expense expense)
    {
      _expenses.Value.InsertOne(expense);
    }

    public void DeleteExpense(string objectId)
    {
      _expenses.Value.DeleteOne(expense => expense.Id == objectId);
    }

    public ICollection<Filter> GetFilters()
    {
      return _filters.Value.Find(new BsonDocument()).ToList();
    }

    public void AddFilter(Filter filter)
    {
      _filters.Value.InsertOne(filter);
    }

    public void DeleteFilter(string objectId)
    {
      _filters.Value.DeleteOne(filter => filter.Id == objectId);
    }

    private static FilterDefinition<Expense> GetFilterDefinition(List<string> filters, string month)
    {
      var builder = Builders<Expense>.Filter;
      var filterDefinition = FilterDefinition<Expense>.Empty;

      if (filters != null && filters.Any())
        filterDefinition = filterDefinition &
                           builder.Regex(x => x.Description, new BsonRegularExpression(string.Join("|", filters), "i"));

      if (month != null && month.Any())
        filterDefinition = filterDefinition & builder.Regex(x => x.Date, new BsonRegularExpression(month));

      return filterDefinition;
    }
  }
}