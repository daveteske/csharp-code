using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            // register the DI services
            var services = new ServiceCollection()
                .AddSingleton<MyApplication>()
                //.AddTransient<MyApplication>()
                .AddTransient<IConcreteService, ConcreteService>()
                .BuildServiceProvider();

            var app = services.GetService<MyApplication>();
            app.Run();
            app = null;

            app = services.GetService<MyApplication>();
            app.Run();
        }

    }

    
}
