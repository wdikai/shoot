using UnityEngine;
using System.Collections;

public class TargetShowBehaviour : MonoBehaviour {

    public GameObject Camera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.SetActive(false);
        }
    }
}
