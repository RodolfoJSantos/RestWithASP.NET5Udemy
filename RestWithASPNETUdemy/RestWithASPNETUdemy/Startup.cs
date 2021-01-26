using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Business.Implementations;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Repository.Implementations;
using Serilog;
using System;
using System.Collections.Generic;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;

namespace RestWithASPNETUdemy
{
	public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment;
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
			Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));


            services.AddControllers();

            var conn = Configuration["MySQLConnection:MySQLConnectionString"];
            services.AddDbContext<MysqlContext>(opt => opt.UseMySql(conn,
                       ServerVersion.AutoDetect(conn)));

			if (Environment.IsDevelopment())
			{
                MigrateDatabase(conn);
			}

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;

                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            })
                .AddXmlSerializerFormatters();
            

            services.AddApiVersioning();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "REST API's From 0 to Azure with ASP .NET Core 5 and Docker",
                        Version = "v1",
                        Description = "API RESTfull developed in course 'REST API's From 0 to Azure with ASP .NET Core 5 and Docker'",
                        Contact = new OpenApiContact
						{
                            Name = "Rodolfo José",
                            Url = new Uri("https://github.com/RodolfoJSantos")
						}
                    });
            });

            services.AddScoped<IPersonBusiness, PersonBusiness>();
            services.AddScoped<IBookBusiness, BookBusiness>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

		

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "REST API's From 0 to Azure with ASP .NET Core 5 and Docker - v1");
            });

			var option = new RewriteOptions();
            option.AddRedirect("^$","swagger");
			app.UseRewriter(option); //redireciona rota par incializar com o recurso swagger

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void MigrateDatabase(string conn)
        {
			try
			{
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(conn);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true
                };
                evolve.Migrate();
			}
			catch (Exception ex)
			{
                Log.Error("Database migration failed ", ex);
				throw;
			}
        }
    }
}
