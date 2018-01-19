using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastForCarScript : MonoBehaviour {

    //define global variables
    public static float sensor1Distance = 0;
    public static GameObject line1Sensor;

    public static float sensor2Distance = 0;
    public static GameObject line2Sensor;

    public static float sensor3Distance = 0;
    public static GameObject line3Sensor;

    //init
    void Start ()
    {
        line1Sensor = new GameObject("Sensor");
        line1Sensor.transform.SetParent(this.gameObject.transform);
        line1Sensor.AddComponent<LineRenderer>();

        line2Sensor = new GameObject("Sensor");
        line2Sensor.transform.SetParent(this.gameObject.transform);
        line2Sensor.AddComponent<LineRenderer>();

        line3Sensor = new GameObject("Sensor");
        line3Sensor.transform.SetParent(this.gameObject.transform);
        line3Sensor.AddComponent<LineRenderer>();
    }
	
	//update
	void Update ()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y + 200, transform.position.z + 350);
        sensor1Distance = CastRay(origin, transform.forward, line1Sensor);
        sensor2Distance = CastRay(origin, transform.right, line2Sensor);
        sensor3Distance = CastRay(origin, -transform.forward, line3Sensor);
    }

    //cast ray
    private float CastRay (Vector3 origin, Vector3 direction, GameObject lineObject)
    {
        LineRenderer line = lineObject.GetComponent<LineRenderer>();

        Ray ray = new Ray(origin, direction);
        RaycastHit hit;
        bool rayHit = Physics.Raycast(ray, out hit, 1000);

        line.startWidth = 10;
        line.endWidth = 10;

        line.SetPosition(0, origin);
        if (rayHit)
        {
            line.SetPosition(1, hit.point);
            return hit.distance;
        }
        else
        {
            line.SetPosition(1, ray.GetPoint(1000));
            return Vector3.Distance(ray.origin, ray.GetPoint(1000));
        }
    }
}
