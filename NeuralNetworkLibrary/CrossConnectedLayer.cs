
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
            //biases[0] = ;
            for (int j = 0; j < outputSize; j++)
            {
                biases[0][j] = random.NextSingle() - 0.5f;
            }
            //weights = initiateWeights(new int[] { inputSize, outputSize })[0];
            //biases = initiateBiases(new int[] { 1, outputSize });
        }
        /*
                    public CrossConnectedLayer(float[][] weights, float[][] biases) : base(inputSize,outputSize)
                    {
                        this.weights = weights;
                        this.biases = biases; //biases is 1,weights[0].length
                        this.inputSize = weights.Length;
                        this.outputSize = weights[0].Length;
                        //Console.WriteLine("input size is " + inputSize);
                        //Console.WriteLine("output size is " + outputSize);
                    }*/
        public override float[] backward(float[] outputError, float learningRate)
        {
            float[] inputError = new float[inputSize]; //shape == input

            /*Console.WriteLine("input length is " + inputError.Length +
                " outputerror length is " + outputError.Length +
                " weights length1 is " + weights.Length +
                " and weights length2 is " + weights[0].Length +
                " and inputsize is "+inputSize +
                " and output size is "+outputSize);*/

            for (int i = 0; i < inputSize; i++)
            {
                for (int k = 0; k < outputSize; k++)
                {
                    //Console.WriteLine("i is " + i + " k is " + k);
                    inputError[i] += (
                        outputError[k] *
                        weights[i][k]);
                }
                //output[i] = input.Select((element, index) => element * weights).ToArray().Sum();
            }



            //Console.WriteLine("input error is " + str(inputError));

            float[][] weightError = new float[inputSize][]; // shape == weighht shape

            for (int i = 0; i < inputSize; i++)
            {
                weightError[i] = new float[outputSize];
                for (int j = 0; j < outputSize; j++)
                {
                    //Console.WriteLine("j is " + j);
                    weightError[i][j] = (
                        input[i] *
                        outputError[j]);
                    //Console.WriteLine("made a weight error to " + input[i] + " * " + outputError[j]);
                }
            }
            //Console.WriteLine("weight error is " + str(weightError));
            //Console.WriteLine("changed from " + str(weights[0].Take(3)) + " to ");
            //Console.WriteLine("orig weights was " + str(weights));
            weights = weights.Select((array, index) => array.Select((element, index2) => element - (weightError[index][index2] * learningRate)).ToArray()).ToArray();
            //Console.WriteLine("now weights is " + str(weights));
            //Console.WriteLine(str(weights[0].Take(3)));
            //Console.WriteLine("what happened " + str(weights[0][0]) + " - " + str(weightError[0][0]) + " * " + learningRate);
            //Console.WriteLine("orig biases was " + str(biases));
            biases[0] = biases[0].Select((element, index) => element - (outputError[index] * learningRate)).ToArray();
            //Console.WriteLine("now biases is " + str(biases));
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
                    //Console.WriteLine("i is " + i + " j is " + j);
                    output[i] += (weights[j][i] * input[j]);
                    //output[i] = input.Select((element, index) => element * weights).ToArray().Sum();
                }
                output[i] += biases[0][i];
            }
            return output;
        }
    }
}
