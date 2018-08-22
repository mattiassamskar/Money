using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Money.Core;
using Money.Core.Models;
using Money.Core.Services;
using Money.Db;

namespace Money.Web
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
      services.AddMvc();
      services.Configure<FormOptions>(x =>
      {
        x.ValueLengthLimit = int.MaxValue;
        x.MultipartBodyLengthLimit = int.MaxValue;
      });

      services.AddMediatR(typeof(Expense).Assembly);
      services.AddSingleton<IDbService, FakeDbService>();
      services.AddSingleton<IStatementService, StatementService>();
      services.AddSingleton<IStatementParser, CirclekStatementParser>();
      services.AddSingleton<IStatementParser, SkandiaStatementParser>();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseStaticFiles();
      app.UseMvc();
    }
  }
}
