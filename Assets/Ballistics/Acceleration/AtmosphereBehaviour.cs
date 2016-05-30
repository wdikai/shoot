using System;
using UnityEngine;

public class AtmosphereBehaviour : MonoBehaviour
{
    private float absolutePressure;
    private float temperature;
    private float gasConstant;

    private float formResistanceCoeficient;
    private float square;
    private float mass;


    private Vector3 accel;

    void Start()
    {
        var properties = transform.GetComponent<BulletProperties>();

        absolutePressure = properties.AbsolutePressureAir * 133.322f;
        temperature = properties.TemperatureAir + 273;
        gasConstant = properties.GasConstant;

        formResistanceCoeficient = properties.BulletFormResistanceCoeficient;
        square = properties.BulletSquare;
        mass = properties.BulletMass;

        var flightBehaviour = GetComponent<FlightBehaviour>();
        if (flightBehaviour != null)
        {
            flightBehaviour.Accelerations.Add(CalculateAtmosphereAcceleration);
        }
    }

    private Vector3 CalculateAtmosphereAcceleration(float time, Vector3 speed)
    {

        var speedSquare = (float)Math.Pow(speed.magnitude, 2);
        var density = absolutePressure / (gasConstant * temperature);
        var direction = speed.normalized * -1;
        var force = formResistanceCoeficient * square * density * speedSquare / 2;
        var acceleration = direction * (force / mass) * time;

        accel = acceleration;

        return acceleration;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(880, 360, 200, 40), new GUIContent("accel: " + accel));
    }
}
