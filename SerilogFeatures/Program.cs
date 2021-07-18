using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SerilogFeatures
{
    public class Program
    {

        public static void Main(string[] args)
        {
            try
            {
                //SERILOG - set a template for a output file log
                var outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}";
                //SERILOG - create a loger setting the configurations above
                Log.Logger = new
                    LoggerConfiguration()
                    .MinimumLevel.Debug()//SERILOG- Configuration with the minimum event severity level to debug
                    .MinimumLevel.Override("Microsoft",LogEventLevel.Information) // SERILOG - Restricted loggin to information level and higher for system "Microsoft"
                    .MinimumLevel.Override("Microsoft.AspNetCore",LogEventLevel.Warning) // SERILOG - Restricted loggin to warning level and higher for system "Microsoft.AspNetCore"
                    .WriteTo.File(Path.Combine("C:\\Logs\\", "Test-Log-.txt"),
                    rollingInterval: RollingInterval.Day, 
                    outputTemplate: outputTemplate,
                    fileSizeLimitBytes: 100000).CreateLogger();
                CreateHostBuilder(args).Build().Run();
            }
            catch
            {
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        //SERILOG - call the extention UseSerilog to load it a provider log
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}
