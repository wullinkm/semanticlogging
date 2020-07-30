using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using System;
using System.IO;
using System.Threading;

namespace LogExample
{
    class Program
    {
        static int Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            var serilogger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            var services = new ServiceCollection();
            services.AddLogging(logBuilder =>
            {
                logBuilder.AddSerilog(serilogger);
            });

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                var worker = new ExampleWorker(logger);
                worker.Start();
                logger.LogInformation("The work finished at: {DateTime}", DateTime.Now);
                return 0;
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "The application crashed");
                return 1;
            }
            finally
            {
                serilogger.Dispose();
            }
        }
    }

   
}
