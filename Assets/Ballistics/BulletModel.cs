using UnityEngine;
using System.Collections;

public class BulletModel : MonoBehaviour {
    public float BulletFormResistanceCoeficient = 0.47f;
    public float BulletMass = 30f;
    public float BulletSquare = 1.256f;
    
    public float TemperatureAir = 30f;
    public float AbsolutePressureAir = 700;
    public float GasConstant = 287.06f;
}
