using System;
using UnityEngine;

public class AtmosphereBehaviour : MonoBehaviour {

    public bool hasAtmosphere = false;
    public float Temperature = 30f;
    public float AbsoluteDensity = 700;
    public float SteamDensity = 1;

    public float FormResistanceCoeficient = 0.47f;
    public float Mass = 30f;
    public float Square = 1.256f;


    private float Rd = 287.06f;
    private float Rv = 461.5f;
    private float pd;
    private float pv;

    void Start()
    {
        pd = AbsoluteDensity * 133.322f;
        pv = SteamDensity * 133.322f;

        var flightBehaviour = GetComponent<FlightBehaviour>();
        if (flightBehaviour != null)
        {
            flightBehaviour.Accelerations.Add(CalculateAtmosphereAcceleration);
        }
    }

    private Vector3 CalculateAtmosphereAcceleration(float time, Vector3 speed)
    {
        if (hasAtmosphere)
        {
            var temp1 = FormResistanceCoeficient * Square;
            var temp2 = 2 * Mass * (Temperature + 273) * Rd * Rv;
            var temp3 = (float)Math.Pow(speed.magnitude, 2) * time;
            var temp4 = Rv * (pd - pv) + Rd * pv;
            var vector = speed.normalized * -1;
            var acceleration = vector * (temp1 / temp2 * temp3 * temp4);

            return acceleration;
        }

        return speed;
    }


}
