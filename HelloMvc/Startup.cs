using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HelloMvc
{
    using System;

    using Services.Implementations;
    using Services;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;

    using HelloMvc.ActionFilters;

    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(
                s =>
                {
                    //passive filter i.e. LogActionFilterAttribute is just a placeholder from LogActionFilter to use.
                    s.Filters.Add(typeof(LogActionFilter));
                });

            // Create the container builder.
            var containerBuilder = new ContainerBuilder();

            // Register dependencies, populate the services from
            // the collection, and build the container.
            containerBuilder.RegisterType<HelloService>().As<IHelloService>();
            containerBuilder.Populate(services);

            var container = containerBuilder.Build();

            // Return the IServiceProvider resolved from the container.
            return container.Resolve<IServiceProvider>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Debug);

            app.UseIISPlatformHandler();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            app.UseMvc();
        }
    }
}