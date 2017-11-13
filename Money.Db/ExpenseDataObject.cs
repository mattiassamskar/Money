using System;
using Money.Models;
using MongoDB.Bson;

namespace Money.Db
{
  public class ExpenseDataObject
  {
    private ExpenseDataObject()
    {
    }

    public ObjectId Id { get; set; }

    public DateTime Date { get; set; }

    public string Description { get; set; }

    public double Amount { get; set; }

    public Expense ToExpense()
    {
      return new Expense
      {
        Date = Date,
        Description = Description,
        Amount = Amount
      };
    }

    public static ExpenseDataObject FromExpense(Expense expense)
    {
      return new ExpenseDataObject
      {
        Date = expense.Date,
        Description = expense.Description,
        Amount = expense.Amount
      };
    }
  }
}