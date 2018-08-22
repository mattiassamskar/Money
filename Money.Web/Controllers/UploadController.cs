using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Money.Core.Notifications;
using Money.Core.Requests;

namespace Money.Web
{
  [ApiController]
  [Route("[controller]")]
  public class UploadController : ControllerBase
  {
    private readonly IMediator _mediator;

    public UploadController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpPost]
    public ActionResult Post()
    {
      var files = Request.Form.Files;

      foreach (var file in files)
      {
        ParseFile(file.OpenReadStream()).Wait();
      }

      return Ok();
    }

    private async Task ParseFile(Stream stream)
    {
      using (var destinationStream = new MemoryStream())
      {
        stream.CopyTo(destinationStream);

        var expenses = await _mediator.Send(new ParsePdfRequest { Bytes = destinationStream.ToArray() });
        await _mediator.Send(new SaveExpensesRequest { Expenses = expenses });
        await _mediator.Publish(new ExpensesNotification { Expenses = expenses });
      }
    }
  }
}