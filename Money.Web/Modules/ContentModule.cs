using Nancy;

namespace Money.Web.Modules
{
  public class ContentModule : NancyModule
  {
    public ContentModule()
    {
      Get["/"] = _ => Response.AsFile("Content/index.html");
    }
  }
}