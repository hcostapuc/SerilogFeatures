using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerilogFeatures
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
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*SERILOG -
             Available as part of the Serilog.AspNetCore package, 
             Serilog’s request logging middleware (i.e., the RequestLoggingMiddleware class) 
             is used to add one summary log message per request in your application. In other words, 
             you can take advantage of the request logging middleware to get a summary of all of the 
             log messages for each request in your application.

             To add RequestLoggingMiddleware to the pipeline, you need to call the UseSerilogRequestLogging() method.
             This method condenses the vital information of each request into a clean and concise completion event.
             
             Now, if you run our application again, you’ll see the same log messages but with an additional summary log message 
             from Serilog that lets you know the time taken to execute the request, as shown example above:
             2021-07-18 17:30:49.953 [Information] HTTP "GET" "/" responded 200 in 293.3897 ms
             */
            app.UseSerilogRequestLogging();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
