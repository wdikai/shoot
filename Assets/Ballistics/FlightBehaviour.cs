using UnityEngine;
using System.Collections.Generic;
using Assets.Ballistics.Acceleration;

public class FlightBehaviour : MonoBehaviour {
    public GameObject ShotDecal;

    public int LifeTime = 10;

    public float Speed = 600f;
    public float StandardDeviation = 0.5f;

    public Vector3 VSpeed;
    public List<Accelaration> Accelerations = new List<Accelaration>();

    private float lifeTime;

    private Vector3 deviation;


    public void init(Transform init)
    {
        lifeTime = 0f;
        gameObject.transform.position = init.position;
        gameObject.transform.rotation = init.rotation;
        gameObject.SetActive(false);

        var deviationX = Random.Range(-StandardDeviation, StandardDeviation);
        var deviationY = Random.Range(-StandardDeviation, StandardDeviation);
        var deviationZ = Random.Range(-StandardDeviation, StandardDeviation);

        deviation = new Vector3(deviationX, deviationY, deviationZ);

        VSpeed = transform.forward * Speed + deviation;
    }
    	
	void FixedUpdate() {
        var time = Time.deltaTime;
        var startPosition = transform.position;

        var newSpeed = VSpeed;

        foreach (var acceleration in Accelerations)
        {
            newSpeed += acceleration(time, VSpeed);
        }

        VSpeed = newSpeed;

        var endPosition = startPosition + VSpeed * time;
        var distance = Vector3.Distance(startPosition, endPosition);
        var direction = (endPosition - startPosition).normalized;
        
        var ray = new Ray(startPosition, direction);
        var colliders = Physics.RaycastAll(ray, distance);
        foreach (var hit in colliders)
        {
            if (hit.collider.tag != "Wind")
            {
                OnHit(hit);
                break;
            }
        }

        transform.forward = direction;
        transform.position = endPosition;
        lifeTime += time;
        if (lifeTime > LifeTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnHit(RaycastHit hit)
    {
        var message = "Bullet collide in position:" + hit.point;
        var hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        Instantiate(ShotDecal, hit.point, hitRotation);
        transform.gameObject.SetActive(false);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(880, 40, 200, 40), new GUIContent("Speed: " + VSpeed));
        GUI.Label(new Rect(880, 80, 200, 40), new GUIContent("Speed: " + VSpeed.magnitude));
        GUI.Label(new Rect(880, 120, 200, 40), new GUIContent("Position: " + transform.position));
        GUI.Label(new Rect(880, 120, 200, 40), new GUIContent("Position: " + transform.position));
        GUI.Label(new Rect(880, 160, 200, 40), new GUIContent("Time: " + lifeTime));
    }
}
