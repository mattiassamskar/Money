using MediatR;
using Nancy;
using Nancy.Conventions;
using Nancy.TinyIoc;

namespace InvoiceParser.Web
{
  public class Bootstrapper:DefaultNancyBootstrapper
  {
    protected override void ConfigureConventions(NancyConventions nancyConventions)
    {
      base.ConfigureConventions(nancyConventions);

      nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory(@"/", @"/Content"));
    }

    protected override void ConfigureApplicationContainer(TinyIoCContainer container)
    {
      base.ConfigureApplicationContainer(container);

      container.Register<IMediator>((x, overloads) => new Mediator(x.Resolve, x.ResolveAll));
    }
  }
}