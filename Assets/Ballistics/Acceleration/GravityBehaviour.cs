using UnityEngine;

public class GravityBehaviour : MonoBehaviour
{
   
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

    private Vector3 CalculateGravity(Vector3 speed)
    {
        return gravity;
    }
}
