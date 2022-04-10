using NeuralNetworkLibrary;
using static NeuralNetworkLibrary.MathFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNeuralNetworkLibrary
{
    public class TestXOR
    {
        public static void testxor()
        {

            float[][] x_train = new float[][] { new float[] { 0, 0 }, new float[] { 0, 1 }, new float[] { 1, 0 }, new float[] { 1, 1 } };
            float[][] y_train = new float[][] { new float[] { 0 }, new float[] { 1 }, new float[] { 1 }, new float[] { 0 } };

            //setup network
            NeuralNetwork net = new NeuralNetwork(mse, mse_prime);
            net.add(new CrossConnectedLayer(2, 3));
            net.add(new FunctionLayer(tanh, tanh_prime));
            net.add(new CrossConnectedLayer(3, 1));
            net.add(new FunctionLayer(tanh, tanh_prime));

            //TRAIN
            net.train(x_train, y_train, 1000, 0.1f);

            //test
            float[] output = net.predict(x_train[0]);
            Console.WriteLine(output);
        }
    }
}
