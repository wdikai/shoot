using UnityEngine;

public class WindInteractionBehaviour : MonoBehaviour
{

    public float Speed = 10;
    public Vector3 Direction = new Vector3(0, 0, 1);

    private Vector3 windSpeed;

    private void Start() {
        windSpeed = Direction.normalized * Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WindInteraction"))
        {
            var windBehaviour = other.GetComponent<WindBehaviour>();
            if (windBehaviour != null) {
                windBehaviour.WindSpeed = windSpeed;
                windBehaviour.HasWind = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WindInteraction"))
        {
            var windBehaviour = other.GetComponent<WindBehaviour>();
            if (windBehaviour != null)
            {
                windBehaviour.WindSpeed = Vector3.zero;
                windBehaviour.HasWind = false;
            }
        }
    }


}
