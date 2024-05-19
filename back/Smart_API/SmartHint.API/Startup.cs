using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using SmartHint.Data.Context.Data;
using SmartHint.Data.Repositories;
using SmartHint.Domain.Interfaces.Repositories;
using SmartHint.Domain.Interfaces.Services;
using SmartHint.Domain.Service;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartHint.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Configuração KWT
            //var secretKey = Configuration["Jwt:SecretKey"];
            //var issuer = Configuration["Jwt:Issuer"];

            services.AddDbContext<DataContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseMySql(Configuration.GetConnectionString("MySqlConnection"),
                                    new MySqlServerVersion(new Version(8, 0, 28)));
            });


            services.AddScoped<IGeralRepo, GeralRepo>();

            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IClienteRepo, ClienteRepo>();

            services.AddScoped<IEnderecoService, EnderecoService>();
            services.AddScoped<IEnderecoRepo, EnderecoRepo>();

            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartHint.API", Version = "v1" });
            });

            services.AddAntiforgery(options => {
                options.HeaderName = "X-CSRF-TOKEN";
                options.FormFieldName = "myFieldName";
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PousadaVovo.API v1"));
            }
            else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            var errorMessage = new
                            {
                                error = "An error occurred while processing your request.",
                                message = error.Error.Message // Exemplo de retorno da mensagem de erro específica
                            };

                            var json = JsonSerializer.Serialize(errorMessage);
                            await context.Response.WriteAsync(json);
                        }

                    });
                });

                app.UseHsts();
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthentication();

            //app.UseAuthorization();

            app.UseCors("AllowAnyOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
