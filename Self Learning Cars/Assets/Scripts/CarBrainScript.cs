using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBrainScript : MonoBehaviour {

    //define global variables
    float engine = 0;
    float turn = 0;

    //start
    void Start ()
    {
		
	}
	
	//update
	void Update ()
    {
        if (Input.GetKey(KeyCode.W)) engine = 5;
        if (Input.GetKey(KeyCode.S)) engine = -5;

        if (Input.GetKey(KeyCode.A)) turn = -0.5F;
        else if (Input.GetKey(KeyCode.D)) turn = 0.5F;
        else turn = 0;

        MoveCar(engine, turn);
	}


    //move car
    public void MoveCar(float engine, float turn)
    {
        Rigidbody body = gameObject.GetComponent<Rigidbody>();

        body.AddTorque(new Vector3(0, turn, 0), ForceMode.Acceleration);
        body.AddForce(transform.right * 100, ForceMode.Impulse);
    }
}
