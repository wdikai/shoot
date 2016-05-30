using UnityEngine;
using System.Collections;

public class TargetShowBehaviour : MonoBehaviour {

    public GameObject Camera;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.SetActive(false);
        }
    }
}
