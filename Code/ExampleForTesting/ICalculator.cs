using System;

namespace ExampleForTesting
{
    public interface ICalculator
    {
        string Mode { get; set; }
        event EventHandler PowerUp;

        int Add(int x, int y);
    }
}