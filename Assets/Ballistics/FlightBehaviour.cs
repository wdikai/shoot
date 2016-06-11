using UnityEngine;
using System.Collections.Generic;
using Assets.Ballistics.Acceleration;

public class FlightBehaviour : MonoBehaviour
{
    public GameObject ShotDecal;

    public Vector3 VSpeed;
    public List<Accelaration> Accelerations = new List<Accelaration>();

    private float lifeTime;
    private float maximumLifeTime;

    private float speed;
    private float standardDeviation;

    public void Init(Transform init)
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
        Accelerations = new List<Accelaration>();
    }

    private void FixedUpdate()
    {
        var time = Time.deltaTime;
        var startPosition = transform.position;

        var newSpeed = VSpeed;

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
        Debug.LogWarning(hit.point + Vector3.forward);
        var hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);        
        Instantiate(ShotDecal, hit.point + (VSpeed.normalized * -0.5f), hitRotation);
        transform.gameObject.SetActive(false);
    }

    //void OnGUI()
    //{
    //    GUI.Label(new Rect(Screen.width - 200, 150, 200, 40), VSpeed.ToString());
    //    GUI.Label(new Rect(Screen.width - 200, 190, 200, 40), "" + VSpeed.magnitude);
    //}
}
