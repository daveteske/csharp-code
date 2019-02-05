using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactoryExample
{
    class Program
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7-1#async-main
        static async Task Main()
        {
            int exampleToRun = 0;
            var htmlReturned = "";
            do
            {
                Console.Clear();
                Console.WriteLine("1: Direct Usage");
                Console.WriteLine("2: Named Client");
                Console.WriteLine("3: Typed Client");
                Console.WriteLine("4: Encapsulated Typed Client");
                Console.WriteLine("Select example to run (1-4):");
                exampleToRun = ((int)Console.ReadKey().Key)-48;
                
            } while (exampleToRun < 1 || exampleToRun > 4);

            Console.WriteLine();
            switch (exampleToRun)
            {
                case 1:
                    Console.WriteLine("Direct Usage");
                    htmlReturned = await DirectUsage();
                    break;
                case 2:
                    Console.WriteLine("Named Client");
                    htmlReturned = await NamedClient();
                    break;
                case 3:
                    Console.WriteLine("Typed Client");
                    htmlReturned = await TypedClient();
                    break;
                case 4:
                    Console.WriteLine("Encapsulated Typed Client");
                    htmlReturned = await EncapsulatedTypedClient();
                    break;
            }

            Console.WriteLine();
            Console.WriteLine(htmlReturned.AsSpan().Slice(start: 94, length: 37).ToString());
            Console.WriteLine();
            Console.WriteLine(htmlReturned.AsSpan().Slice(start: 20054, length: 250).ToString());
            Console.WriteLine();
            Console.WriteLine("*** Press Enter to Exit ***");
            Console.ReadLine();
        }

        static async Task<string> DirectUsage()
        {
            // This goes on in Startup.cs when using and ASP.NET project
            var services = new ServiceCollection();
            services.AddHttpClient();

            // The next 2 linesa are taken care of by the built in DI
            var serviceProvider = services.BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

            // The remainder would be part of the controller or service
            var client = httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://en.wikipedia.org");

            return await client.GetStringAsync("/wiki/AltaVista");
        }

        static async Task<string> NamedClient()
        {
            // Startup.cs
            var services = new ServiceCollection();
            services.AddHttpClient("wikipedia", c =>
            {
                c.BaseAddress = new Uri("https://en.wikipedia.org");
                c.DefaultRequestHeaders.Add("User-agent", "DavesDeveloperNotes");
            });

            // built in DI
            var serviceProvider = services.BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

            // service / controller
            var client = httpClientFactory.CreateClient("wikipedia");

            return await client.GetStringAsync("/wiki/AltaVista");

        }

        static async Task<string> TypedClient()
        {
            // startup
            var services = new ServiceCollection();
            services.AddHttpClient<WikiClient>();

            // DI
            var serviceProvider = services.BuildServiceProvider();

            // service / controller
            var client = serviceProvider.GetRequiredService<WikiClient>();

            return await client.Client.GetStringAsync("/wiki/AltaVista");
        }

        static async Task<string> EncapsulatedTypedClient()
        {
            // startup
            var services = new ServiceCollection();
            services.AddHttpClient<IEncapsulatedWikiClient, EncapsulatedWikiClient>();

            // DI
            var serviceProvider = services.BuildServiceProvider();

            // Controller / Service
            var client = serviceProvider.GetRequiredService<IEncapsulatedWikiClient>();

            return await client.GetAltaVistaArticle();
        }
    }

    public class WikiClient
    {
        public HttpClient Client { get; }

        public WikiClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://en.wikipedia.org");
            httpClient.DefaultRequestHeaders.Add("User-agent", "DavesDeveloperNotes");
            Client = httpClient;
        }
    }


    public interface IEncapsulatedWikiClient
    {
        Task<string> GetAltaVistaArticle();
    }

    public class EncapsulatedWikiClient : IEncapsulatedWikiClient
    {
        private HttpClient _client { get; }

        public EncapsulatedWikiClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://en.wikipedia.org");
            httpClient.DefaultRequestHeaders.Add("User-agent", "DavesDeveloperNotes");
            _client = httpClient;
        }

        public async Task<string> GetAltaVistaArticle()
        {
            return await _client.GetStringAsync("/wiki/AltaVista");
        }
    }
}
