using BuildingWebApisWithAspNet.DbContexts;
using BuildingWebApisWithAspNet.Extensions;
using BuildingWebApisWithAspNet.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterServices();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        string[] origins = builder.Configuration.GetSection("Options").GetSection("AllowedOrigins").Value.Split(';');
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

builder.Services.AddDbContext<MyBgListContext>((options) =>
{
    var connString = builder.Configuration.GetConnectionString("MyBgList");
    options.UseSqlServer(connString);
});

builder.Services.AddExceptionHandler<ExceptionHandlerMiddleware>();
builder.Services.AddProblemDetails();
builder.Services.AddHttpLogging(options =>
{

});

var app = builder.Build();

app.UseHttpLogging();
// Configure the HTTP request pipeline.
if (app.Configuration.GetValue<bool>("UseSwagger"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Configuration.GetValue<bool?>("UseDeveloperExceptionPage") ?? false)
    app.UseDeveloperExceptionPage();
else
    app.UseExceptionHandler("/error");

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();

app.UseMinimalApiEndPoints();

app.MapControllers();

app.RegisterMiddlewares();

app.UseStatusCodePages();


app.Run();
