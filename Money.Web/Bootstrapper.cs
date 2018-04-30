using MediatR;
using Money.Core;
using Money.Core.Models;
using Money.Core.Services;
using Money.Db;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.StructureMap;
using Nancy.Conventions;
using Nancy.Json;
using StructureMap;

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
      JsonSettings.MaxJsonLength = 1000000;
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
        cfg.For<IDbService>().Add<MongoDbService>().Singleton();
        cfg.For<IStatementService>().Add<StatementService>().Singleton();
        cfg.For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => ctx.GetInstance);
        cfg.For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => ctx.GetAllInstances);
        cfg.For<IMediator>().Use<Mediator>();
      });
    }
  }
}