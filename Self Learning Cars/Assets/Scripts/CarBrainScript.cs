using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBrainScript : MonoBehaviour {

    //define global variables

    //start
    void Start ()
    {

	}
	
	//update
	void Update ()
    {
        float x = 0;
        float z = 0;

        if (Input.GetKey(KeyCode.W)) x = -30;
        else if (Input.GetKey(KeyCode.S)) x = 30;
        if (Input.GetKey(KeyCode.A)) z = 30;
        else if (Input.GetKey(KeyCode.D)) z = -30;

        Camera.main.transform.position += new Vector3(z, 0, x);
	}


    //move car
    public void MoveCar(float engine, float turn)
    {
        Rigidbody body = gameObject.GetComponent<Rigidbody>();

        body.AddRelativeTorque(new Vector3(0, turn, 0) / 50, ForceMode.VelocityChange);
        body.AddRelativeForce(new Vector3(engine, 0, 0) * 50, ForceMode.Acceleration);
    }
}
