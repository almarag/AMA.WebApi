namespace AMA.WebApi
{
    using System;
    using AMA.Common.Filters;
    using AMA.Common.Interfaces;
    using AMA.Persistence.Contexts;
    using AMA.Persistence.Models;
    using AMA.Users;
    using AMA.Users.Domain.Interfaces;
    using AMA.Users.Domain.Repositories;
    using AutoMapper;
    using FluentValidation.AspNetCore;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

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

            services.AddAutoMapper(userApplicationAssembly);

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateRequestFilter));
            })
            .AddFluentValidation(
                x => x.RegisterValidatorsFromAssembly(userApplicationAssembly)
            );

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("MySqlConnectionString")));

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
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
