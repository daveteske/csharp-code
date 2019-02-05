using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactory
{
    class Program
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7-1#async-main
        static async Task Main()
        {
            int count = 5;
            string label = "Colors used in the map";
            var pair = (count, label); // element names are "count" and "label"

            Console.WriteLine("Starting!");

            var html = await DirectUsage();

            Console.WriteLine(html);

            Console.ReadLine();
        }

        static async Task<string> DirectUsage()
        {
            var services = new ServiceCollection().AddHttpClient();

            var serviceProvider = services.BuildServiceProvider();

            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

            var wikipedia = httpClientFactory.CreateClient();

            var data = await wikipedia.GetStringAsync("https://en.wikipedia.org/wiki/AltaVista");

            return data;
        }

        static void NamedClient()
        {

        }

        static void TypedClient()
        {

        }
    }
}
