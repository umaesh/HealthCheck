using HealthCheck.Core;
using HealthCheck.Windows.Checkers;
using HealthCheck.Windows.Metrics;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncContext.Run(() => MainAsync(args));
        }
        static async void MainAsync(string[] args)
        {
            var healthCheck = new HealthCheck.Core.HealthCheck(new IChecker[]
{
            new SystemDriveHasFreeSpace(new AvailableSystemDriveSpace())
});
            var result = await healthCheck.Run();

            Console.WriteLine(result.Passed); // true/false
            Console.WriteLine(result.Status); // "success"/"failure"
            foreach (var checkResult in result.Results)
            {
                Console.WriteLine(checkResult.Checker); // E.g. "System drive has free space"
                Console.WriteLine(checkResult.Passed);  // true/false
                Console.WriteLine(checkResult.Output);  // E.g. "123.4 GB available"
                Console.ReadKey();
            }

        }
    }
}
