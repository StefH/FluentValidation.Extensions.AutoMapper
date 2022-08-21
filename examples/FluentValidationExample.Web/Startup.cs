using AutoMapper;
using FluentValidation;
using FluentValidation.Extensions.AutoMapper;
using FluentValidationExample.Web.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentValidationExample.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();

            services.AddBusiness();

            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(GlobalExceptionFilter));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMapper autoMapper)
        {
            ConfigureAutoMapperAndFluentValidation(autoMapper);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void ConfigureAutoMapperAndFluentValidation(IMapper mapper)
        {
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            var resolver = new FluentValidationPropertyNameResolver(mapper);
            ValidatorOptions.Global.PropertyNameResolver = resolver.Resolve;
        }
    }
}
