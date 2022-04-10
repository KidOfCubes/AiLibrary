
namespace NeuralNetworkLibrary
{
    public class NeuralNetwork
    {
        private static Random random = new Random();

        Func<float[], float[], float> loss;
        Func<float[], float[], float[]> lossDerivative;

        public List<Layer> layers = new List<Layer>();

        public NeuralNetwork(Func<float[], float[], float> loss, Func<float[], float[], float[]> loss_prime)
        {
            this.loss = loss;
            this.lossDerivative = loss_prime;
        }
        public void add(Layer layer) {
            layers.Add(layer);
        }

        //predict
        public float[] predict(float[] input_data) {
            int samples = input_data.Length;

            //push input_data forward through layers
            float[] output = input_data;
            foreach (Layer layer in layers) {
                output = layer.forward(output);
            }
            return output;
        }

        //training
        public void train(float[][] x_train, float[][] y_train, int epochs, float learning_rate) {
            int samples = (x_train.Length);

            //training loop
            for (int i = 0; i < epochs; i++)
            {

                float err = 0;
                for (int j = 0; j < samples; j++) //loop through data
                {

                    float[] output = x_train[j];
                    foreach (Layer layer in layers) {
                        output = layer.forward(output);
                    }

                    //compute loss to show
                    err += loss(y_train[j], output);

                    //push error backward to change weights

                    float[] error = lossDerivative(y_train[j], output);

                    for (int k = layers.Count; k-- > 0;)
                    {
                        error = layers[k].backward(error, learning_rate);
                    }
                }
                //calculate avg error
                err /= samples;

                Console.WriteLine("epoch "+(i + 1) +"/"+ epochs + "   error="+err);
            }
        }





    }
}