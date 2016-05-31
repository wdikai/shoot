using System;
using UnityEngine;

public class WindBehaviour : MonoBehaviour
{
    private float absolutePressure;
    private float temperature;
    private float gasConstant;

    private float formResistanceCoeficient;
    private float square;
    private float mass;

    public float Speed = 0;
    public bool HasWind = false;
    public Vector3 Direction = Vector3.zero;

    void Start()
    {
        Speed = 0;
        HasWind = false;
        Direction = Vector3.zero;

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
            flightBehaviour.Accelerations.Add(CalculateWindAcceleration);
        }
    }

    private Vector3 CalculateWindAcceleration(float time, Vector3 speed)
    {
        if (HasWind)
        {
            var speedSquare = (float)Math.Pow(Speed, 2);
            var density = absolutePressure / (gasConstant * temperature);
            var force = formResistanceCoeficient * square * density * speedSquare / 4;
            var acceleration = Direction * (force / mass) * time;

            return acceleration;
        }

        return Vector3.zero;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(880, 240, 200, 40), new GUIContent("Speed: " + Speed));
        GUI.Label(new Rect(880, 280, 200, 40), new GUIContent("Direction: " + Direction));
        GUI.Label(new Rect(880, 320, 200, 40), new GUIContent("HasWind: " + HasWind));
    }
}
