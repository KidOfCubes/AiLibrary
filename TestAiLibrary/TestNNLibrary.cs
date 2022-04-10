using NeuralNetworkLibrary;
using static NeuralNetworkLibrary.MathFunctions;

namespace TestNeuralNetworkLibrary
{
    public class TestNNLibrary
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("testLibrary START");
            int testType = 1;
            switch (testType)
            {
                case 0:
                    TestXOR.testxor();
                    break;
                case 1:
                    TestMNIST.testminst();
                    break;
                default:
                    break;
            }
        }
    }

}