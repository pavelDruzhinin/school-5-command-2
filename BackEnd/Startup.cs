using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChatsConstructor.WebApi.Hubs;
using ChatsConstructor.WebApi.Models;
using ChatsConstructor.WebApi.Models.Domains;
using ChatsConstructor.WebApi.Models.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.IO;
using SwaggerSettings = ChatsConstructor.WebApi.Settings.SwaggerSettings;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Swagger;

namespace BackEnd
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
            services.AddDbContext<ChatsConstructorContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            services.AddIdentity<User, IdentityRole<Guid>>(opts => {
                opts.Password.RequiredLength = 5;   // минимальная длина
                opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
                opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
                opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
                opts.Password.RequireDigit = false; // требуются ли цифры
            })
            .AddEntityFrameworkStores<ChatsConstructorContext>();
            services.ConfigureApplicationCookie(c=>{
                c.Events.OnRedirectToLogin = context=>{
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });
            //services.AddMvc();
            services.AddSignalR();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Web API",
                    Version = "v1",

                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                //var comments = new XPathDocument(xmlPath);
                //c.OperationFilter<XmlCommentsOperationFilter>(comments);
                //c.SchemaFilter<XmlCommentsSchemaFilter>(comments);


            });
        }

       
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseSwagger();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            //var config = new HttpConfiguration();
            //var xmlFormatter = config.Formatters.XmlFormatter;

            // Starts using XmlSerialiser rather than DataContractSerializer.
            //xmlFormatter.UseXmlSerializer = true;

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            var swaggerSettings = new SwaggerSettings();

            Configuration.GetSection(nameof(swaggerSettings)).Bind(swaggerSettings);

            app.UseSwagger(option =>
            {
                option.RouteTemplate = swaggerSettings.JsonRoute;
            });

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerSettings.UiEndpoint, swaggerSettings.Description);
                option.RoutePrefix = "WebApi/swagger";
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chat");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
