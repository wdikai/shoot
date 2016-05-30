using UnityEngine;

public class WindInteractionBehaviour : MonoBehaviour
{

    public float Speed = 10;
    public Vector3 Direction = new Vector3(0, 0, 1);

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WindInteraction"))
        {
            var windBehaviour = other.GetComponent<WindBehaviour>();
            if (windBehaviour != null) {
                windBehaviour.Speed = Speed;
                windBehaviour.Direction = Direction.normalized;
                windBehaviour.HasWind = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WindInteraction"))
        {
            var windBehaviour = other.GetComponent<WindBehaviour>();
            if (windBehaviour != null)
            { 
                windBehaviour.HasWind = false;
            }
        }
    }


}
