using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Money.Web.Notifications;
using Money.Web.Requests;
using Nancy;

namespace Money.Web.Modules
{
  public class UploadModule : NancyModule
  {
    private readonly IMediator _mediator;

    public UploadModule(IMediator mediator) : base("/upload")
    {
      _mediator = mediator;
      Post["/"] = _ =>
      {
        var file = Request.Files.FirstOrDefault();

        if (file == null)
          return Negotiate.WithStatusCode(HttpStatusCode.BadRequest);

        ParseFile(file.Value).Wait();

        return Negotiate.WithStatusCode(HttpStatusCode.OK);
      };
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