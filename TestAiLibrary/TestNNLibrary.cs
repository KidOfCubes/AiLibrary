using NeuralNetworkLibrary;
//using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;
//using System.Numerics.Tensors;
using static NeuralNetworkLibrary.NeuralNetwork;
using static NeuralNetworkLibrary.MathFunctions;
/*using Keras.Datasets;
using Keras;
using Numpy;
using Keras.Utils;
using Python.Runtime;*/

namespace TestNeuralNetworkLibrary
{
    public class TestNNLibrary
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("testLibrary");
            int testType = 1;
            switch (testType)
            {
                case 0:
                    testxor();
                    break;
                case 1:
                    TestMNIST.testminst();
                    break;
                default:
                    break;
            }
            //testxor();



        }

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
/*        public static Image CreateImage(float[][] data)
        {
            float min = 0;
            float max = 1;
            float range = max - min;
            byte v;

            Bitmap bm = new Bitmap(data.Length, data[0].Length);

            Console.WriteLine("test");
            for (int j = 0; j < bm.Height; j++)
            {
                for (int i = 0; i < bm.Width; i++)
                {
                    bm.SetPixel(i, j, Color.FromArgb((int)(data[i][j] * 255), (int)(data[i][j] * 255), (int)(data[i][j] * 255)));
                }
            }
            MemoryStream stream = new MemoryStream();
            bm.Save(stream, ImageFormat.Png);
            byte[] bytes = stream.ToArray();
            Console.WriteLine(Convert.ToBase64String(bytes));
            return bm;

        }*/






/*        public static string str(Object thing)
        {
            return JsonConvert.SerializeObject(thing);
        }*/

    }

}