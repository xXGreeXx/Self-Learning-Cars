using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork {

    //define global varaibles
    public List<List<Perceptron>> perceptrons = new List<List<Perceptron>>();
    public NeuralNetworkMemoryBank MemoryBank = new NeuralNetworkMemoryBank();

    //constructor
    public NeuralNetwork(int inputLength, int[] hiddenLengths, int outputLength)
    {

    }

    //forward prop
    public float[] ForwardProp(float[] inputsToFeed)
    {
        float[] inputs = inputsToFeed;



        return inputs;
    }

    //back prop
    public void BackProp(float[] inputsToFeed, float reward)
    {

    }

    //decide reward
    public float DecideReward(float[] inputsToFeed, float[] outputsToFeed)
    {
        float reward = 0;


        return reward;
    }
}
