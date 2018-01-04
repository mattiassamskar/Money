using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Money.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Money.Db
{
  public class MongoDbService : IDbService
  {
    private readonly Lazy<IMongoCollection<Expense>> _collection;

    public MongoDbService()
    {
      var connectionString = ConfigurationManager.AppSettings["ConnectionString"];

      _collection = new Lazy<IMongoCollection<Expense>>(() =>
        new MongoClient(connectionString).GetDatabase("money").GetCollection<Expense>("expenses"));
    }

    public ICollection<Expense> GetExpenses()
    {
      return _collection.Value.Find(new BsonDocument()).ToList();
    }

    public ICollection<Expense> GetFilteredExpenses(IEnumerable<string> filters, string month)
    {
      return _collection.Value.FindSync(GetFilterDefinition(filters.ToList(), month)).ToList();
    }

    public void AddExpenses(IEnumerable<Expense> expenses)
    {
      expenses.ToList().ForEach(AddExpense);
    }

    public void AddExpense(Expense expense)
    {
      if (!ExpenseExists(expense))
        _collection.Value.InsertOne(expense);
    }

    public void DeleteExpense(string objectId)
    {
      _collection.Value.DeleteOne(expense => expense.Id == objectId);
    }

    private bool ExpenseExists(Expense expense)
    {
      var builder = Builders<Expense>.Filter;
      var filter = builder.Eq(e => e.Date, expense.Date) &
                   builder.Eq(e => e.Description, expense.Description) &
                   builder.Eq(e => e.Amount, expense.Amount);

      return _collection.Value.Count(filter) > 0;
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