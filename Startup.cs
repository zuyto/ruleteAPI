using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rulete.Helpers;
using rulete.Persistence.IRepository;
using rulete.Persistence.Repository;
using Swashbuckle.AspNetCore.Swagger;

namespace rulete
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton<IRuletteRepository, RuletteRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddCors(o => o.AddPolicy("AllowClientApp", builder =>
            {
                builder.WithOrigins("http://localhost:44396", "http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "RuletteAPI", Version = "v1" });
                c.CustomSchemaIds((type) => type.ToString()
                    .Replace("[", "_")
                    .Replace("]", "_")
                    .Replace(",", "-")
                    .Replace("`", "_")
                );
            });
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowClientApp"));
            });
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("es-CO"),
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("es-CO"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RuletteAPI");
                c.RoutePrefix = string.Empty;
            });
            app.UseMvc();
            app.UseCors("AllowClientApp");
        }
    }
}
