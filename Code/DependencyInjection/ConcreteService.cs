
namespace DependencyInjection
{
    public class ConcreteService : IConcreteService
    {
        private int previousValue = 0;

        public int Increment(int a)
        {
            previousValue += a;

            return previousValue;
        }
    }
}
