namespace Assets.Ballistics.Acceleration.Utils
{
    using UnityEngine;

    public class ForceUtil
    {
        public static Vector3 AirForce(Vector3 speed, float formResistanceCoeficient, float absolutePressure, float square, float temperature, float gasConstant)
        {
            var speedSquare = Mathf.Pow(speed.magnitude, 2);
            var density = absolutePressure / (gasConstant * temperature);
            var direction = speed.normalized * -1;

            return direction * formResistanceCoeficient * square * density * speedSquare / 2;
        }
    }
}
