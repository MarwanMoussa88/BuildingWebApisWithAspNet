using BuildingWebApisWithAspNet.Appsettings;
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
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddOptions<AppSettingsOptions>(AppSettingsOptions.Options)
                .ValidateOnStart();
            return services;
        }

    }
}
