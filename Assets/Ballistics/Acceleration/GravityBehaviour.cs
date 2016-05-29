using UnityEngine;
using System.Collections;

public class GravityBehaviour : MonoBehaviour {

    public bool hasGravity = true;
    public float Gravity = 9.8f;

    private Vector3 gravity;
    void Start ()
    {
        gravity = Vector3.down * Gravity;

        var flightBehaviour = GetComponent< FlightBehaviour>();
        if (flightBehaviour != null)
        {
            flightBehaviour.Accelerations.Add(CalculateSpeed);
        }
    }

    private Vector3 CalculateSpeed(float time, Vector3 speed)
    {
        if (hasGravity)
        {
            return speed + (gravity * time);
        }

        return speed;
    }
}
