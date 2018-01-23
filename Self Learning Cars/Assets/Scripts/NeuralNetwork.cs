using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork {

    //define global varaibles
    public List<List<Perceptron>> perceptrons = new List<List<Perceptron>>();
    public NeuralNetworkMemoryBank MemoryBank = new NeuralNetworkMemoryBank();
    public static readonly float LearningRate = 0.1F;

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
    public void BackProp(float[] inputsToFeed, float[] target)
    {
        float[] inputs = inputsToFeed;

        //prop inputs forward
        float[] outFinal = ForwardProp(inputs);

        //backprop over layers
        for (int layerPropIndex = perceptrons.Count - 1; layerPropIndex > 1; layerPropIndex--)
        {
            List<Perceptron> propLayer = perceptrons[layerPropIndex];
            
            for (int neuronPropIndex = 0; neuronPropIndex < propLayer.Count; neuronPropIndex++)
            {
                Perceptron neuron = propLayer[neuronPropIndex];

                float[] inputsToGive = new float[perceptrons[layerPropIndex - 1].Count];
                for (int indexOfNeuronToPullInputFrom = 0; indexOfNeuronToPullInputFrom < perceptrons[layerPropIndex - 1].Count; indexOfNeuronToPullInputFrom++)
                {
                    inputsToGive[neuronPropIndex] = perceptrons[layerPropIndex - 1][indexOfNeuronToPullInputFrom].lastOutput;
                }

                if (layerPropIndex == perceptrons.Count - 1)
                {
                    neuron.train(inputsToGive, target[neuronPropIndex], false, LearningRate);
                }
                else
                {
                    float combinedError = 0;
                    foreach (Perceptron p in perceptrons[layerPropIndex + 1])
                        combinedError += p.lastError * p.weights[neuronPropIndex];

                    neuron.train(inputsToGive, combinedError, true, LearningRate);
                }
            }
        }

    }

    //decide reward
    public float DecideReward(float[] inputsToFeed, float[] outputsToFeed)
    {
        float reward = -1;


        return reward;
    }
}
