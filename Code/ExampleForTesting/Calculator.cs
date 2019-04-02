using System;

namespace ExampleForTesting
{
    public class Calculator : ICalculator
    {
        public string Mode { get; set; }
        public event EventHandler PowerUp;

        public int Add(int x, int y)
        {
            return x + y;
        }
    }
}
