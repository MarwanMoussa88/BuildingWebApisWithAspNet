using BuildingWebApisWithAspNet.DbContexts;
using BuildingWebApisWithAspNet.Extensions;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddDbContext<MyBgListContext>((options) =>
{
    var connString = builder.Configuration.GetConnectionString("MyBgList");
    options.UseSqlServer(connString);
});
var app = builder.Build();

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

app.UseAuthorization();

app.UseMinimalApiEndPoints();

app.MapControllers();

app.RegisterMiddlewares();


app.Run();
