using Money.Models;
using Money.StatementParsers;
using MediatR;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Money.Db;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.StructureMap;
using Nancy.Conventions;
using StructureMap;
using StructureMap.Graph;

namespace Money.Web
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

      GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => new HubActivator(container));

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
        cfg.For<IDbService>().Add<MongoDbService>().Singleton();
        cfg.For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => ctx.GetInstance);
        cfg.For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => ctx.GetAllInstances);
        cfg.For<IMediator>().Use<Mediator>();
      });
    }
  }
}