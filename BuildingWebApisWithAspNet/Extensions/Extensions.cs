using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using BuildingWebApisWithAspNet.Middlewares;

namespace BuildingWebApisWithAspNet.Extensions
{
    public static class Extensions
    {

        public static IEndpointRouteBuilder UseMinimalApiEndPoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapGet("api/test", () =>
            {
                return new { id = 1, name = "marwan" };
            }).RequireCors("AnyOrigion");

            endpointRouteBuilder.MapGet("error", () =>
            {
                return Results.Problem();
            }).RequireCors("AnyOrigion");

            endpointRouteBuilder.MapGet("error/test", () =>
            {
                throw new Exception("test");
            }).RequireCors("AnyOrigion");

            endpointRouteBuilder.MapGet("COD", () =>
            {
                return Results.Text(
                    """
                     <script>
                     window.alert("This is an alert message!");
                     </script>
                     <noscript>Your Client does not support javascript </noscript>
                    """, "text/html");
            });

            return endpointRouteBuilder;
        }

        public static IApplicationBuilder RegisterMiddlewares(this IApplicationBuilder app)
        {

            app.UseMiddleware<SampleMiddleware>();

            return app;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<SampleMiddleware>();
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddControllers();
            services.AddApiVersioning(options =>
            {
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(options =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach(var version in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(version.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = $"MyBgList {version.ApiVersion}",
                        Version = version.ApiVersion.ToString()
                    });
                }
            });



            return services;
        }

    }
}
