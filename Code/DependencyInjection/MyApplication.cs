using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public class MyApplication
    {
        private readonly IConcreteService concreteService;


        public MyApplication(IConcreteService concreteService)
        {
            this.concreteService = concreteService;
        }

        public void Run()
        {
            for (int i = 0; i < 5; i++)
            {
                var result = concreteService.Increment(1);
                Console.WriteLine($"Result is {result}");
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
