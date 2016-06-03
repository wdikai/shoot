using Assets.Ballistics.Acceleration.Utils;
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

    private void Start()
    {
        var flightBehaviour = GetComponent<FlightBehaviour>();
        if (flightBehaviour != null)
        {
            var properties = transform.GetComponent<BulletProperties>();
            if (properties != null)
            {

                absolutePressure = PressureConverter.MmHgToPascal(properties.AbsolutePressureAir);

                temperature = TemeperatureConverter.CelsiusToKelvin(properties.TemperatureAir);
                temperature = temperature != 0 ? temperature : BulletProperties.DefaultTemperature;

                gasConstant = properties.GasConstant != 0 ? properties.GasConstant : BulletProperties.DefaultGasConstant;

                formResistanceCoeficient = properties.BulletFrontFormResistanceCoeficient;
                square = properties.BulletSquare;

                mass = properties.BulletMass != 0 ? properties.BulletMass : BulletProperties.DefaultBulletMass;

                flightBehaviour.Accelerations.Add(CalculateAtmosphereAcceleration);
            }
        }
    }

    private Vector3 CalculateAtmosphereAcceleration(Vector3 speed)
    {
        var force = ForceUtil.AirForce(speed, formResistanceCoeficient, absolutePressure, square, temperature, gasConstant);
        var acceleration = (force / mass);

        return acceleration;
    }
}
