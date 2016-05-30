using UnityEngine;
using System.Collections;

public class RotateCameraBehaviour : MonoBehaviour
{
    public Transform target;
    public float Distance = 5.0f;
    public float XSpeed = 10.0f;
    public float YSpeed = 1.0f;


    private float xAxis;
    private float yAxis;

    private void Start() {
        xAxis = -25;
        yAxis = -25;
}

    private void Update()
    {
        if(xAxis < 25)
        xAxis += XSpeed * Time.deltaTime;
        if (yAxis < 25)
            yAxis += YSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(yAxis, xAxis, 0.0f);
        transform.rotation = rotation;
        transform.position = target.position + rotation * new Vector3(0.0f, 0.0f, -Distance);
}

}
