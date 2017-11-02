using InvoiceParser.Handlers;
using InvoiceParser.Models;
using InvoiceParser.StatementParsers;
using MediatR;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.StructureMap;
using Nancy.Conventions;
using StructureMap;
using StructureMap.Graph;

namespace InvoiceParser.Web
{
  public class Bootstrapper : StructureMapNancyBootstrapper
  {
    protected override void ConfigureConventions(NancyConventions nancyConventions)
    {
      base.ConfigureConventions(nancyConventions);

      nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory(@"/", @"/Content"));
    }

    protected override void ApplicationStartup(IContainer container, IPipelines pipelines)
    {
      base.ApplicationStartup(container, pipelines);

      container.Configure(cfg =>
      {
        cfg.Scan(scanner =>
        {
          scanner.Assembly(typeof(Expense).Assembly);
          scanner.TheCallingAssembly();
          scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
          scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<>));
          scanner.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
          scanner.AddAllTypesOf<IStatementParser>();
        });
        cfg.For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => ctx.GetInstance);
        cfg.For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => ctx.GetAllInstances);
        cfg.For<IMediator>().Use<Mediator>();
      });
    }
  }
}