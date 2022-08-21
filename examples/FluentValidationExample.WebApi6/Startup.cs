using AutoMapper;
using FluentValidation;
using FluentValidation.Extensions.AutoMapper;

namespace FluentValidationExample.WebApi6;

public class Startup
{
    public IConfiguration Configuration
    {
        get;
    }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureAndRun(WebApplication app, IWebHostEnvironment env, IMapper mapper)
    {
        ConfigureAutoMapperAndFluentValidation(mapper);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void ConfigureAutoMapperAndFluentValidation(IMapper mapper)
    {
        mapper.ConfigurationProvider.AssertConfigurationIsValid();

        var resolver = new FluentValidationPropertyNameResolver(mapper);
        ValidatorOptions.Global.PropertyNameResolver = resolver.Resolve;
    }
}