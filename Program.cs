using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace dotnetmvctest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] urls = new string[] {
                "http://0.0.0.0:5000" // Needed for Docker
            };
            if(System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                urls.Append("http://localhost:5000");
            }

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseUrls(urls)
                .Build();

            host.Run();
        }
    }
}
