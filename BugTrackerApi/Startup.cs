using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using BugTrackerApi.Configurations;
using BugTrackerPersistence.Context;

namespace BugTrackerApi {
  public class Startup {
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services) {
      services.AddDbContext<BugTrackerDbContext>(
        options => options.UseNpgsql(
          Configuration.GetConnectionString("DefaultConnection")
        )
      );

      services.AddControllers().AddNewtonsoftJson(
        opts => opts.SerializerSettings.ReferenceLoopHandling =
          Newtonsoft.Json.ReferenceLoopHandling.Ignore
      );

      services.ResolveDependencies();
      services.AddAutoMapper(typeof(Startup));
      services.AddAuthConfiguration(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseCors(
        opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
      );

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
      });
    }
  }
}
