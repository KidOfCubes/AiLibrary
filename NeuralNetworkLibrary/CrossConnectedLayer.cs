
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    public class CrossConnectedLayer : Layer
    {
        public static Random random = new Random();
        public float[][] weights;
        public float[][] biases;
        public int inputSize;
        public int outputSize;
        public CrossConnectedLayer(int inputSize, int outputSize)
        {
            this.inputSize = inputSize;
            this.outputSize = outputSize;

            weights = new float[inputSize][];
            for (int i = 0; i < inputSize; i++)
            {
                weights[i] = new float[outputSize];
                for (int j = 0; j < outputSize; j++)
                {
                    weights[i][j] = random.NextSingle() - 0.5f;
                }
            }

            biases = new float[][] { new float[outputSize] };
            for (int j = 0; j < outputSize; j++)
            {
                biases[0][j] = random.NextSingle() - 0.5f;
            }
        }
        public override float[] backward(float[] outputError, float learningRate)
        {
            float[] inputError = new float[inputSize]; //shape == input

            for (int i = 0; i < inputSize; i++)
            {
                for (int k = 0; k < outputSize; k++)
                {
                    inputError[i] += (
                        outputError[k] *
                        weights[i][k]);
                }
            }




            float[][] weightError = new float[inputSize][]; // shape == weighht shape

            for (int i = 0; i < inputSize; i++)
            {
                weightError[i] = new float[outputSize];
                for (int j = 0; j < outputSize; j++)
                {
                    weightError[i][j] = (
                        input[i] *
                        outputError[j]);
                }
            }

            weights = weights.Select((array, index) => array.Select((element, index2) => element - (weightError[index][index2] * learningRate)).ToArray()).ToArray();

            biases[0] = biases[0].Select((element, index) => element - (outputError[index] * learningRate)).ToArray();
            return inputError;
        }

        public override float[] forward(float[] input)
        {
            this.input = input;
            float[] output = new float[outputSize];
            for (int i = 0; i < outputSize; i++)
            {
                for (int j = 0; j < inputSize; j++)
                {
                    output[i] += (weights[j][i] * input[j]);
                }
                output[i] += biases[0][i];
            }
            return output;
        }
    }
}
