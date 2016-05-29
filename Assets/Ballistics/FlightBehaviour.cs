using UnityEngine;
using System.Collections.Generic;
using Assets.Ballistics.Acceleration;

public class FlightBehaviour : MonoBehaviour {
    public GameObject ShotDecal;

    public int LifeTime = 10;

    public float Speed = 600f;

    public Vector3 VSpeed;

    private float lifeTime;

    public List<Accelaration> Accelerations = new List<Accelaration>(); 

    public void init(Transform init)
    {
        lifeTime = 0f;
        gameObject.transform.position = init.position;
        gameObject.transform.rotation = init.rotation;
        gameObject.SetActive(false);

        VSpeed = transform.forward * Speed;
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
        
        RaycastHit hit;
        var ray = new Ray(startPosition, direction);
        var collide = Physics.Raycast(ray, out hit, distance);
        if (collide)
        {
            var message = "Bullet collide in position:" + hit.point;
            Debug.LogWarning(message);
            var hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            Instantiate(ShotDecal, hit.point, hitRotation);
            transform.gameObject.SetActive(false);
        }

        transform.forward = direction;
        transform.position = endPosition;
        lifeTime += time;
        if (lifeTime > LifeTime)
        {
            gameObject.SetActive(false);
        }
    }
    void OnGUI()
    {
        GUI.Label(new Rect(880, 40, 200, 40), new GUIContent("Speed: " + VSpeed));
        GUI.Label(new Rect(880, 80, 200, 40), new GUIContent("Speed: " + VSpeed.magnitude));
        GUI.Label(new Rect(880, 120, 200, 40), new GUIContent("Position: " + transform.position));
        GUI.Label(new Rect(880, 160, 200, 40), new GUIContent("Time: " + lifeTime));
    }
}
