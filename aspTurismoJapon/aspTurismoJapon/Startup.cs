using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspTurismoJapon.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace aspTurismoJapon
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ServicioListas>();
            services.AddMvc();

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => 
            //{
            //    options.LoginPath = "/Account/Index/";
            //    options.AccessDeniedPath = "/Account/Denied/";
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Environment = env;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            //app.UseAuthentication();

            app.UseFileServer();
            app.UseMvc(routes => 
            {
                routes.MapRoute(
                       name: "default",
                     template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
