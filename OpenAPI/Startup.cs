using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RestSharp;

namespace OpenAPI
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

            services.AddResponseCompression();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "OpenAPI",
                    Version = "v1",
                    Description = "OpenAPI (formerly Swagger) compliant web service that abstracts away two downstream APIs; the Chuck Norris API and the Star Wars API.",
                    TermsOfService = null,
                    Contact = new OpenApiContact { Name = "Olympas Mkhabela", Email = string.Empty, Url = new Uri("https://twitter.com/OlympusThrone") }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((swagger, httpReq) =>
                 {
                     swagger.Servers = new List<OpenApiServer> 
                     { 
                         new OpenApiServer { Url = $"https://{httpReq.Host.Value}" },
                         new OpenApiServer { Url = $"http://{httpReq.Host.Value}" }
                     };
                     swagger.Tags = new List<OpenApiTag>
                     {
                         new OpenApiTag { Name = "Chuck", Description = "Chuck Norris joke categories"},
                         new OpenApiTag{Name="Swapi", Description = "Star Wars people"},
                         new OpenApiTag{Name="Search"}
                     };
                 });
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenAPI V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
