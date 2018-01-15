using System;
using System.Globalization;
using System.Linq;
using MediatR;
using Money.Web.Requests;
using Nancy;

namespace Money.Web.Modules
{
  public class ExpensesModule : NancyModule
  {
    public ExpensesModule(IMediator mediator) : base("/expenses")
    {
      Get["/"] = _ =>
      {
        string filterString = Request.Query["filter"];
        string month = Request.Query["month"];

        if (!ParametersAreValid(filterString, month))
          return HttpStatusCode.BadRequest;

        var filters = filterString?.Split(',') ?? Enumerable.Empty<string>();

        return mediator.Send(new GetExpensesRequest { Filters = filters, Month = month }).Result;
      };

      Delete["/{id}"] = parameters => mediator.Send(new DeleteExpenseRequest { Id = parameters.id });
    }

    private static bool ParametersAreValid(string filterString, string month)
    {
      return string.IsNullOrEmpty(month) || DateTime.TryParseExact(month, "yyyy-MM",
               CultureInfo.InvariantCulture, DateTimeStyles.None, out var _);
    }
  }
}