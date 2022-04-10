using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    public class MathFunctions
    {
        public static float sigmoid(float Z)
        {
            float A = (float)(1 / (1 + Math.Pow(Math.E,(Z * -1))));

            return A;
        }
        public static float relu(float Z)
        {
            float A = Math.Max(0, Z);

            return A;
        }

        public static float relu_backward(float dA)
        {
            float dZ = dA;
            if (relu(dA) <= 0)
            {
                dZ = 0;
            }


            return dZ;
        }
        public static float sigmoid_backward(float dA)
        {

            float s = (float)(1 / (1 + Math.Pow(Math.E,(-1 * sigmoid(dA)))));

            float dZ = dA * s * (1 - s);

            return dZ;
        }
        public static float tanh(float x)
        {
            return (float)Math.Tanh(x);
        }

        public static float tanh_prime(float x)
        {
            return (float)(1 - Math.Pow(Math.Tanh(x), 2));
        }

        public static float mse(float[] y_true, float[] y_pred)
        {
            return (power(minus(y_true, y_pred), 2)).Average();
        }
        public static float[] mse_prime(float[] y_true, float[] y_pred)
        {
            //#print("shape thing is "+str((2*(y_pred-y_true)/y_true.size).shape))
            return divide(multiply((minus(y_pred, y_true)), 2), y_true.Length);
        }
        public static float[] plus(float[] input1, float[] input2)
        {
            return input1.Select((element, index) => input1[index] + input2[index]).ToArray();
        }
        public static float[] minus(float[] input1, float[] input2)
        {
            return input1.Select((element, index) => input1[index] - input2[index]).ToArray();
        }
        public static float[] multiply(float[] input1, float input2)
        {
            return input1.Select((element, index) => input1[index] * input2).ToArray();
        }
        public static float[] divide(float[] input1, float input2)
        {
            return input1.Select((element, index) => input1[index] / input2).ToArray();
        }
        public static float[] power(float[] input1, float input2)
        {
            return input1.Select((element, index) => (float)Math.Pow(input1[index], input2)).ToArray();
        }
    }
}
