using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBrainScript : MonoBehaviour {

    //define global variables
    NeuralNetwork brain;
    public static float MaxSpeed = 15.5F;
    public static float MaxTurn = 0.25F;

    //start
    void Start ()
    {
        brain = new NeuralNetwork(3, new int[] { 10, 10 }, 2);
	}
	
	//update
	void FixedUpdate ()
    {
        //TOTALLY NOT RELATED move camera with WASD
        float x = 0;
        float z = 0;

        if (Input.GetKey(KeyCode.W)) x = -30;
        else if (Input.GetKey(KeyCode.S)) x = 30;
        if (Input.GetKey(KeyCode.A)) z = 30;
        else if (Input.GetKey(KeyCode.D)) z = -30;

        Camera.main.transform.position += new Vector3(z, 0, x);

        //simulate neural network
        SimulateNetwork();
	}

    //handle network simulation
    public void SimulateNetwork()
    {
        //forward prop network
        float[] inputs = new float[] { RaycastForCarScript.sensor1Distance, RaycastForCarScript.sensor2Distance, RaycastForCarScript.sensor3Distance };
        float[] outputs = brain.ForwardProp(inputs);

        //decide reward
        float reward = brain.DecideReward(inputs, outputs);

        //backprop data
        if (reward > 0)
        {
            while (reward > 0)
            {
                brain.BackProp(new float[] { Random.Range(-30F, 30F), Random.Range(-30F, 30F), Random.Range(-30F, 30F) }, outputs);

                reward--;
            }
        }
        else if(reward < 0)
        {
            while (reward < 0)
            {
                brain.BackProp(inputs, new float[] { Random.Range(-15F, 15F), Random.Range(-15F, 15F) });

                reward++;
            }
        }

        //add output to memory bank
        brain.MemoryBank.AddMemory(inputs, outputs);

        //move car
        float engine = outputs[0];
        float turn = outputs[1];

        if (engine > MaxSpeed) engine = MaxSpeed;
        if (engine < -MaxSpeed) engine = -MaxSpeed;
        if (turn > MaxTurn) turn = MaxTurn;
        if (turn < -MaxTurn) turn = -MaxTurn;

        MoveCar(engine, turn);
    }

    //move car
    public void MoveCar(float engine, float turn)
    {
        Rigidbody body = gameObject.GetComponent<Rigidbody>();
        
        body.AddRelativeTorque(new Vector3(0, turn, 0) / 50, ForceMode.VelocityChange);
        body.AddRelativeForce(new Vector3(engine, 0, 0) * 50, ForceMode.Acceleration);
    }
}
