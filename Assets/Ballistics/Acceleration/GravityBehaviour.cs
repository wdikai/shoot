using UnityEngine;

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
            flightBehaviour.Accelerations.Add(CalculateGravity);
        }
    }

    private Vector3 CalculateGravity(float time, Vector3 speed)
    {
        if (hasGravity)
        {
            return (gravity * time);
        }

        return speed;
    }
}
