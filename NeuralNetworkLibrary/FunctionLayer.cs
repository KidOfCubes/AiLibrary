
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    public class FunctionLayer : Layer
    {
        Func<float, float> function;
        Func<float, float> derivative;
        public FunctionLayer(Func<float, float> function, Func<float, float> derivative)
        {
            this.function = function;
            this.derivative = derivative;
        }

        public override float[] forward(float[] input)
        {
            this.input = input;
            float[] output = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = function(input[i]);
            }
            return output;
        }

        public override float[] backward(float[] outputError, float learningRate)
        {
            float[] inputDerivative = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                inputDerivative[i] = derivative(input[i]);
            }
            float[] returnValue = new float[inputDerivative.Length];
            for (int i = 0; i < inputDerivative.Length; i++)
            {
                returnValue[i] = inputDerivative[i] * outputError[i];
            }
            return returnValue;
        }
    }
}
