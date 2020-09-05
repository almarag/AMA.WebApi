using System;
using AMA.Users;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AMA.WebApi
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
            services.AddControllers();
            AddSwagger(services);

            var userApplicationAssembly = new UsersAssemblyApplication().GetAssembly();
            var userApplicationAssemblyType = userApplicationAssembly.GetType();

            services.AddMediatR(userApplicationAssembly);

            services.AddMvc()
                .AddFluentValidation(
                    x => x.RegisterValidatorsFromAssemblyContaining(userApplicationAssemblyType)
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AMA WebApi V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"AMA WebApi {groupName}",
                    Version = groupName,
                    Description = "AMA WebApi",
                    Contact = new OpenApiContact
                    {
                        Name = "Alejandro Martinez",
                        Email = string.Empty,
                        Url = new Uri("https://almarag.com/"),
                    }
                });
            });
        }
    }
}
