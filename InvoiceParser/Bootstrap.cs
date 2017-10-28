using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediatR;
using MediatR.Pipeline;
using SimpleInjector;

namespace InvoiceParser
{
  public class Bootstrap
  {
    public static IMediator BuildMediator()
    {
      var container = new Container();
      var assemblies = GetAssemblies().ToArray();
      container.RegisterSingleton<IMediator, Mediator>();

      container.Register(typeof(IRequestHandler<,>), assemblies);
      container.Register(typeof(IAsyncRequestHandler<,>), assemblies);
      container.Register(typeof(IRequestHandler<>), assemblies);
      container.Register(typeof(IAsyncRequestHandler<>), assemblies);
      container.Register(typeof(ICancellableAsyncRequestHandler<>), assemblies);
      container.RegisterCollection(typeof(INotificationHandler<>), assemblies);
      container.RegisterCollection(typeof(IAsyncNotificationHandler<>), assemblies);
      container.RegisterCollection(typeof(ICancellableAsyncNotificationHandler<>), assemblies);
      container.RegisterSingleton(Console.Out);

      container.RegisterSingleton(new SingleInstanceFactory(container.GetInstance));
      container.RegisterSingleton(new MultiInstanceFactory(container.GetAllInstances));

      container.RegisterCollection(typeof(IPipelineBehavior<,>), Enumerable.Empty<Type>());
      container.RegisterCollection(typeof(IRequestPreProcessor<>), Enumerable.Empty<Type>());
      container.RegisterCollection(typeof(IRequestPostProcessor<,>), Enumerable.Empty<Type>());
      container.Verify();

      var mediator = container.GetInstance<IMediator>();

      return mediator;
    }

    private static IEnumerable<Assembly> GetAssemblies()
    {
      yield return typeof(Models.Expense).GetTypeInfo().Assembly;
    }
  }
}