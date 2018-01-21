using System.Collections.Generic;

public class NeuralNetworkMemoryBank {

    //define global variables
    List<float[]> inputs = new List<float[]>();
    List<float[]> outputs = new List<float[]>();

    //constructor
    public NeuralNetworkMemoryBank()
    {

    }

    //add memory
    public void AddMemory(float[] input, float[] output)
    {
        inputs.Add(input);
        outputs.Add(output);
    }

    //remove memory
    public void RemoveMemory(int index)
    {
        inputs.RemoveAt(index);
        outputs.RemoveAt(index);
    }
}
