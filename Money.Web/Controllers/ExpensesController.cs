using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Money.Core.Models;
using Money.Core.Requests;

namespace Money.Web
{
  [ApiController]
  [Route("[controller]")]
  public class ExpensesController : ControllerBase
  {
    private readonly IMediator _mediator;

    public ExpensesController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Expense>> Get(string filter, string month)
    {
      if (!ParametersAreValid(filter, month))
        return BadRequest();

      var filters = filter?.Split(',') ?? Enumerable.Empty<string>();
      var result = _mediator.Send(new GetExpensesRequest { Filters = filters, Month = month }).Result;

      return Ok(result);
    }

    private static bool ParametersAreValid(string filterString, string month)
    {
      return string.IsNullOrEmpty(month) || DateTime.TryParseExact(month, "yyyy-MM", 
        CultureInfo.InvariantCulture, DateTimeStyles.None, out var _);
    }
  }
}