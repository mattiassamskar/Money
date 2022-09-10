using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Money.Core;
using Money.Core.Models;
using Money.Core.Services;
using Money.Db;

namespace Money.Web
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<Options>(Configuration);
      services.AddMvc(x => x.EnableEndpointRouting = false);
      services.AddMediatR(typeof(Expense).Assembly);
      services.AddMediatR(typeof(Filter).Assembly);
      services.AddSingleton<IDbService, MongoDbService>();
      services.AddSingleton<IStatementService, StatementService>();
      services.AddSingleton<IStatementParser, CirclekStatementParser>();
      services.AddSingleton<IStatementParser, SkandiaStatementParser>();
      services.AddSingleton<IStatementParser, SeomStatementParser>();
      services.AddSingleton<IStatementParser, SkekraftStatementParser>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseDefaultFiles();
      app.UseStaticFiles();
      app.UseMvc();
    }
  }
}
