using Owin;

namespace Money.Web
{
  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      app.MapSignalR();
    }
  }
}