using System;

namespace Money.Models
{
  public class Expense
  {
    public DateTime Date { get; set; }

    public string Description { get; set; }

    public double Amount { get; set; }
  }
}