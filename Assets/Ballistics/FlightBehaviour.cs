using UnityEngine;
using System.Collections.Generic;
using Assets.Ballistics.Acceleration;

public class FlightBehaviour : MonoBehaviour
{
    public GameObject ShotDecal;

    private Vector3 VSpeed;
    public List<Accelaration> Accelerations = new List<Accelaration>();

    private float lifeTime;
    private float deltaTime;
    private float maximumLifeTime;

    private float speed;
    private float standardDeviation;

    public void init(Transform init)
    {
        lifeTime = 0f;

        gameObject.transform.position = init.position;
        gameObject.transform.rotation = init.rotation;
        gameObject.SetActive(false);

        var deviationX = Random.Range(-standardDeviation, standardDeviation);
        var deviationY = Random.Range(-standardDeviation, standardDeviation);
        var deviationZ = Random.Range(-standardDeviation, standardDeviation);

        var deviation = new Vector3(deviationX, deviationY, deviationZ);

        VSpeed = transform.forward * speed + deviation;
    }

    private void Start()
    {
        var properties = transform.GetComponent<BulletProperties>();
        if (properties != null)
        {
            maximumLifeTime = properties.LifeTime;
            speed = properties.Speed;
            standardDeviation = properties.StandardDeviation;
        }
        else
        {
            maximumLifeTime = BulletProperties.DefaultLifeTime;
            speed = BulletProperties.DefaultSpeed;
            standardDeviation = BulletProperties.DefaultStandardDeviation;
        }
    }

    private void FixedUpdate()
    {
        var time = Time.deltaTime;
        deltaTime = time;
        var startPosition = transform.position;

        var newSpeed = VSpeed;
        var squareTime = Mathf.Pow(time, 2);

        foreach (var acceleration in Accelerations)
        {
            newSpeed += acceleration(VSpeed) * time;
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
        if (lifeTime > maximumLifeTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnHit(RaycastHit hit)
    {
        var hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        Instantiate(ShotDecal, hit.point, hitRotation);
        transform.gameObject.SetActive(false);
    }
}
