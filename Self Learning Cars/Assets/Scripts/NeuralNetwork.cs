using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork {

    //define global varaibles
    public List<List<Perceptron>> perceptrons = new List<List<Perceptron>>();
    public NeuralNetworkMemoryBank MemoryBank = new NeuralNetworkMemoryBank();
    public static readonly float LearningRate = 0.1F;
    public static float CurrentValueOfNetwork = 0;

    //constructor
    public NeuralNetwork(int inputLength, int[] hiddenLengths, int outputLength)
    {
        //input layer
        perceptrons.Add(new List<Perceptron>() { new Perceptron(0, 10), new Perceptron(0, 10), new Perceptron(0, 10) });

        //hidden layers
        perceptrons.Add(new List<Perceptron>() { new Perceptron(3, 10), new Perceptron(3, 10), new Perceptron(3, 10), new Perceptron(3, 10), new Perceptron(3, 10),
                                                 new Perceptron(3, 10), new Perceptron(3, 10), new Perceptron(3, 10), new Perceptron(3, 10), new Perceptron(3, 10)});
        perceptrons.Add(new List<Perceptron>() { new Perceptron(10, 2), new Perceptron(10, 2), new Perceptron(10, 2), new Perceptron(10, 2), new Perceptron(10, 2),
                                                 new Perceptron(10, 2), new Perceptron(10, 2), new Perceptron(10, 2), new Perceptron(10, 2), new Perceptron(10, 2)});

        //output layer
        perceptrons.Add(new List<Perceptron>() { new Perceptron(10, 1), new Perceptron(10, 1) });
    }

    //forward prop
    public float[] ForwardProp(float[] inputsToFeed)
    {
        float[] inputs = inputsToFeed;

        //iterate over layers
        for (int layerIndex = 1; layerIndex < perceptrons.Count; layerIndex++)
        {
            List<Perceptron> layer = perceptrons[layerIndex];
            float[] inputsBuffer = new float[layer.Count];

            //iterate over neurons
            for (int neuronIndex = 0; neuronIndex < layer.Count; neuronIndex++)
            {
                inputsBuffer[neuronIndex] = layer[neuronIndex].output(inputs);
            }

            inputs = inputsBuffer;
        }


        return inputs;
    }

    //back prop
    public void BackProp(float[] inputsToFeed)
    {
        float[] inputs = inputsToFeed;

        //iterate over layers
        for (int layerIndex = perceptrons.Count - 1; layerIndex > 1; layerIndex--)
        {
            List<Perceptron> layer = perceptrons[layerIndex];

            //get inputs up to date
            for (int layerIndexToProp = 1; layerIndexToProp < layerIndex; layerIndexToProp++)
            {
                List<Perceptron> layerToProp = perceptrons[layerIndexToProp];
                float[] propInputsBuffer = new float[layerToProp.Count];

                for (int neuronIndexToProp = 0; neuronIndexToProp < layerToProp.Count; neuronIndexToProp++)
                {
                    propInputsBuffer[neuronIndexToProp] = layerToProp[neuronIndexToProp].output(inputs);
                }

                inputs = propInputsBuffer;
            }

            //iterate over neurons
            for (int neuronIndex = 0; neuronIndex < layer.Count; neuronIndex++)
            {
                Perceptron p = layer[neuronIndex];

                if (layerIndex == perceptrons.Count - 1)
                {
                    if (CurrentValueOfNetwork > 0)
                    {
                        for (int i = 0; i < CurrentValueOfNetwork; i++)
                        {
                            float output = p.output(inputs);
                            float[] inputsToUse = new float[inputs.Length];
                            for (int ind = 0; ind < inputsToUse.Length; ind++)
                            {
                                inputsToUse[ind] = Random.Range(0.0F, 1.0F);
                            }

                            p.train(inputsToUse, output, false, LearningRate);
                        }
                    }
                    else
                    {
                        p.train(inputs, Random.Range(CurrentValueOfNetwork, -CurrentValueOfNetwork), false, LearningRate);
                    }
                }
                else
                {
                    float error = 0;
                    foreach (Perceptron neuronToPullError in perceptrons[layerIndex + 1])
                    {
                        error += neuronToPullError.lastError;
                    }
                    p.train(inputs, error, true, LearningRate);
                }
            }
        }
    }

    //decide reward
    public void DecideReward(float[] inputsToFeed, float[] outputsToFeed)
    {
        float reward = 0;


        CurrentValueOfNetwork = reward;
    }
}
