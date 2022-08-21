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

    public void ConfigureAndRun(WebApplication app, IWebHostEnvironment env)
    {
        ConfigureAutoMapperAndFluentValidation(app);

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

    private static void ConfigureAutoMapperAndFluentValidation(WebApplication app)
    {
        var mapper = app.Services.GetRequiredService<IMapper>();
        mapper.ConfigurationProvider.AssertConfigurationIsValid();

        var resolver = new FluentValidationPropertyNameResolver(mapper);
        ValidatorOptions.Global.PropertyNameResolver = resolver.Resolve;
    }
}