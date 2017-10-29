using Nancy;

namespace InvoiceParser.Web
{
  public class ContentModule : NancyModule
  {
    public ContentModule()
    {
      Get["/"] = _ => Response.AsFile("Content/index.html");
    }
  }
}