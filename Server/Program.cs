using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Blazor8s.Server
{
    /// <summary>
    /// 
    /// Main() was refactored to remove extraneous stuff,
    /// under the opinion of David Pine.
    ///
    /// - removed Public,
    /// - added Task vs. void, followed by RunAsync() because async is available.
    /// - eliminated the separate CreateHostBuilder() by moving it into Main()
    /// 
    /// Other refacctoring opinions (like removing the entire Main(), and leavin only
    /// one statement) were ignored due to complexity in readability. 
    /// 
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .Build()
                .RunAsync();
        }
    }
}
