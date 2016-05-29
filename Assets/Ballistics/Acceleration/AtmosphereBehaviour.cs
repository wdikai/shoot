using System;
using UnityEngine;

public class AtmosphereBehaviour : MonoBehaviour
{
    public float Temperature = 30f;
    public float AbsolutePressure = 700;

    public float FormResistanceCoeficient = 0.47f;
    public float Mass = 30f;
    public float Square = 1.256f;
    public float GasConstant = 287.06f;


    private float absolutePressure;
    private float temperature;

    void Start()
    {
        absolutePressure = AbsolutePressure * 133.322f;
        temperature = Temperature + 273;

        var flightBehaviour = GetComponent<FlightBehaviour>();
        if (flightBehaviour != null)
        {
            flightBehaviour.Accelerations.Add(CalculateAtmosphereAcceleration);
        }
    }

    private Vector3 CalculateAtmosphereAcceleration(float time, Vector3 speed)
    {
        var speedSquare = (float)Math.Pow(speed.magnitude, 2) * time;
        var density = absolutePressure / (GasConstant * temperature);
        var vector = speed.normalized * -1;
        var force = FormResistanceCoeficient * Square * density * speedSquare / 2;
        var acceleration = vector * (force / Mass);

        return acceleration;
    }


}
