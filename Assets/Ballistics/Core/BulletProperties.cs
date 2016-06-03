using UnityEngine;

public class BulletProperties : MonoBehaviour
{
    public float LifeTime = 10;

    public float Speed = 600f;

    public float StandardDeviation = 0.5f;

    public float BulletFrontFormResistanceCoeficient = 0.04f;
    public float BulletSideFormResistanceCoeficient = 0.47f;

    public float BulletMass = 30f;
    public float BulletSquare = 1.256f;
    
    public float TemperatureAir = 30f;
    public float AbsolutePressureAir = 700;
    public float GasConstant = 287.06f;


    public const float DefaultLifeTime = 10f;
    public const float DefaultSpeed = 500f;
    public const float DefaultStandardDeviation = 0.5f;

    public const float DefaultTemperature = 30f;
    public const float DefaultBulletMass = 0.001f;
    public const float DefaultGasConstant = 287.06f;

}
