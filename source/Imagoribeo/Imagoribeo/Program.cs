using Imagoribeo.Analyze;
using Imagoribeo.Global;
using Imagoribeo.Interfaces.Analyze;
using Imagoribeo.Interfaces.Global;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Imagoribeo
{
    class Program
    {
        static void Main(string[] args)
        {
            var loggerFactory = new LoggerFactory().AddConsole().AddDebug();
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogInformation("Imagoribeo started");

            //----------------------
            var services = new ServiceCollection();
            services.AddSingleton<IFileInfoIO, FileInfoIO>();
            services.AddSingleton<ISystemIO, SystemIO>();
            services.AddSingleton<IAnalyzer, Analyzer>();
            var serviceProvider = services.BuildServiceProvider();

            logger.LogInformation("Imagoribeo configured");
            //----------------------

            var analyzer = serviceProvider.GetService<IAnalyzer>();


            var watch = Stopwatch.StartNew();
            var result2 = analyzer.Directory(@"F:\_Transfer");
            analyzer.CleanUp(@"F:\_Transfer");
            watch.Stop();

            logger.LogInformation("needed time: " + watch.ElapsedMilliseconds);

            logger.LogInformation("Imagoribeo end");
        }
    }
}