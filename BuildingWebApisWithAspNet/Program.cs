using Asp.Versioning.ApiExplorer;
using BuildingWebApisWithAspNet.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterServices();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        string[] origins = builder.Configuration.GetValue<string[]>("AllowedOrigins");
        policy.WithOrigins(origins);
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });

    options.AddPolicy("AnyOrigion", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Configuration.GetValue<bool>("UseSwagger"))
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                $"MyBgList {description.ApiVersion}"
            );
        }
    });
}

if (app.Configuration.GetValue<bool?>("UseDeveloperExceptionPage") ?? false)
    app.UseDeveloperExceptionPage();
else
    app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMinimalApiEndPoints();

app.MapControllers();

app.RegisterMiddlewares();


app.Run();
