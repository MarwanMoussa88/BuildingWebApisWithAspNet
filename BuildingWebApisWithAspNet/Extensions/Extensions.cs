using BuildingWebApisWithAspNet.Appsettings;
using BuildingWebApisWithAspNet.Middlewares;
using Microsoft.EntityFrameworkCore;

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
            services.AddControllers(options =>
            {
                options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(
                (x) => $"The value '{x}' is invalid.");
                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(
                (x) => $"The field {x} must be a number.");
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
                (x, y) => $"The value '{x}' is not valid for {y}.");
                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(
                () => $"A value is required.");
            });

            services.AddOptions<AppSettingsOptions>(AppSettingsOptions.Options)
                .ValidateOnStart();
            return services;
        }

        public static IQueryable<T> OrderByColumn<T>(this IQueryable<T> query, string columnName, bool isDescending = false) where T : class
        {
            Type type = typeof(T);
            var propertyNames = new HashSet<string>(type.GetProperties().Select(c => c.Name));

            if (!propertyNames.Contains(columnName.ToLower()))
                columnName = type.GetProperties()[0].Name;

            if (isDescending)
                return query.OrderByDescending(c => EF.Property<T>(c, columnName));
            else
                return query.OrderBy(keySelector: c => EF.Property<T>(c, columnName));
        }

    }
}
