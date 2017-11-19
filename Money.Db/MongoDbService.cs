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
    private readonly Lazy<IMongoCollection<ExpenseDataObject>> _collection;

    public MongoDbService()
    {
      var connectionString = ConfigurationManager.AppSettings["ConnectionString"];

      _collection = new Lazy<IMongoCollection<ExpenseDataObject>>(() =>
        new MongoClient(connectionString).GetDatabase("money").GetCollection<ExpenseDataObject>("expenses"));
    }

    public ICollection<Expense> GetExpenses()
    {
      return _collection.Value.Find(new BsonDocument()).ToList().Select(edo => edo.ToExpense()).ToList();
    }

    public void AddExpenses(IEnumerable<Expense> expenses)
    {
      expenses.ToList().ForEach(AddExpense);
    }

    public void AddExpense(Expense expense)
    {
      if (!ExpenseExists(expense))
        _collection.Value.InsertOne(ExpenseDataObject.FromExpense(expense));
    }

    private bool ExpenseExists(Expense expense)
    {
      var builder = Builders<ExpenseDataObject>.Filter;
      var filter = builder.Eq(e => e.Date, expense.Date) &
                   builder.Eq(e => e.Description, expense.Description) &
                   builder.Eq(e => e.Amount, expense.Amount);

      return _collection.Value.Count(filter) > 0;
    }
  }
}