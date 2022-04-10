using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    public abstract class Layer
    {
        public float[]? input;
        public abstract float[] forward(float[] input);
        public abstract float[] backward(float[] outputError, float learningRate); //output error is shape 1,outputSize
    }
}
