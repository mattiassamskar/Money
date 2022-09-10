using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Money.Core.Models;
using Money.Core.Requests;

namespace Money.Web
{
  [ApiController]
  [Route("[controller]")]
  public class FiltersController : ControllerBase
  {
    private readonly IMediator _mediator;

    public FiltersController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Filter>> Get()
    {
      var result = _mediator.Send(new GetFiltersRequest()).Result;
      return Ok(result);
    }

    [HttpPut]
    public ActionResult Put(string text)
    {
      _mediator.Send(new AddFilterRequest { Text = text }).Wait();
      return Ok();
    }

    [HttpDelete]
    public ActionResult Delete(string id)
    {
      _mediator.Send(new DeleteFilterRequest { Id = id }).Wait();
      return Ok();
    }
  }
}