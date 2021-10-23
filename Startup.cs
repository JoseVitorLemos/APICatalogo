using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using APICatalogo.Services;
using APICatalogo.Filter;
using APICatalogo.Validations;
using APICatalogo.Logging;

namespace APICatalogo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ApiLoggingFilter>();
            services.AddDbContext<DatabaseContext>(options => { 
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection")));
            });

            services.AddTransient<IMeuServico,MeuServico>();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
         
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "APICatalogo", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APICatalogo v1"));
            } 

            loggerFactory.AddProvider(new CustomLoggerProvider( new CustomLoggerProviderConfiguration
            {
               LogLevel = LogLevel.Information 
            }));

            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
