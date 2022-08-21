using System.Reflection;
using FluentValidationExample.WebApi6;
using FluentValidationExample.WebApi6.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(config =>
{
    config.Filters.Add(typeof(GlobalExceptionFilter));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddBusiness();
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(Program)));

var app = builder.Build();

var startup = new Startup(builder.Configuration);
startup.ConfigureAndRun(app, builder.Environment);