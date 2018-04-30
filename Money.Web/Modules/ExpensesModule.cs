using System;
using System.Globalization;
using System.Linq;
using MediatR;
using Money.Core.Requests;
using Nancy;

namespace Money.Web.Modules
{
  public class ExpensesModule : NancyModule
  {
    private readonly IMediator _mediator;

    public ExpensesModule(IMediator mediator) : base("/expenses")
    {
      _mediator = mediator;

      Get["/"] = _ =>
      {
        string filterString = Request.Query["filter"];
        string month = Request.Query["month"];

        if (!ParametersAreValid(filterString, month))
          return HttpStatusCode.BadRequest;

        var filters = filterString?.Split(',') ?? Enumerable.Empty<string>();

        return _mediator.Send(new GetExpensesRequest { Filters = filters, Month = month }).Result;
      };

      Delete["/{id}"] = parameters => _mediator.Send(new DeleteExpenseRequest { Id = parameters.id });
    }

    private static bool ParametersAreValid(string filterString, string month)
    {
      return string.IsNullOrEmpty(month) || DateTime.TryParseExact(month, "yyyy-MM",
               CultureInfo.InvariantCulture, DateTimeStyles.None, out var _);
    }
  }
}