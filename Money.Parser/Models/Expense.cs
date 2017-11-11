using System;
using MediatR;

namespace Money.Models
{
  public class Expense : INotification
  {
    public DateTime Date { get; set; }

    public string Description { get; set; }

    public double Amount { get; set; }
  }
}