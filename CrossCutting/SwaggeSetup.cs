using Microsoft.OpenApi.Models;

namespace WEBAPI.CrossCutting
{
    public static class SwaggerSetup
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            return services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Atividade DIO",
                    Version = "v1",
                    Description = "Sistema para atividade da DIO",
                    Contact = new OpenApiContact
                    {
                        Name = "Raphael Santos",
                        Email = "raphaelfreitassantos7@gmail.com"
                    }
                });

                var xmlPath = Path.Combine("wwwroot", "Api-Doc.xml");
                opt.IncludeXmlComments(xmlPath);
            });
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            return app.UseSwagger().UseSwaggerUI(c =>
            {
                c.RoutePrefix = "documentation";
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "API v1");
            });
        }
    }
}
