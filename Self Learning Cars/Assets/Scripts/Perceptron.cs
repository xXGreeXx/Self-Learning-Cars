using UnityEngine;

public class Perceptron
{
    //define global variables
    public float[] weights { get; private set; }
    public int outputs;
    public float bias = 0.001F;

    public float lastError;
    public float lastOutput;

    //constructor
    public Perceptron(int numberOfInputs, int numberOfOutputs)
    {
        //randomize weights
        weights = new float[numberOfInputs];
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = Random.Range(-1F, 1F);
        }

        //set outputs
        outputs = numberOfOutputs;
    }

    //output
    public float output(float[] inputs)
    {
        float answer = 0;

        for (int i = 0; i < inputs.Length; i++)
        {
            answer += weights[i] * inputs[i];
        }

        answer += bias;

        lastOutput = activation(answer);
        return lastOutput;
    }

    //train
    public void train(float[] inputs, float target, bool takeTargetAsError, float lr)
    {
        float answer = output(inputs);
        float error = takeTargetAsError ? target : target - answer;
        float combinedError = 0;

        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] += inputs[i] * error * lr;
            combinedError += inputs[i] * error;
        }
        bias += combinedError * lr;

        lastError = error;
    }

    //activation //ReLU\\
    public float activation(float x)
    {
        return Mathf.Max(0, x);
    }
}