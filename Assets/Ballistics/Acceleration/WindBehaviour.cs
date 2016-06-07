using Assets.Ballistics.Acceleration.Utils;
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

    public bool Enable = true;

    public bool HasWind = false;

    public Vector3 WindSpeed = Vector3.zero;

    private void Start()
    {
        var flightBehaviour = GetComponent<FlightBehaviour>();
        if (flightBehaviour != null)
        {
            HasWind = false;
            WindSpeed = Vector3.zero;

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

                flightBehaviour.Accelerations.Add(CalculateWindAcceleration);
            }
        }
    }

    private Vector3 CalculateWindAcceleration(Vector3 speed)
    {
        if (Enable && HasWind)
        {
            var force = ForceUtil.AirForce(WindSpeed, formResistanceCoeficient, absolutePressure, square, temperature, gasConstant);
            var acceleration = (force / mass);

            return acceleration;
        }

        return Vector3.zero;
    }
}
