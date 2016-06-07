using UnityEngine;
using System.Collections;

public class TargetShowBehaviour : MonoBehaviour {

    public GameObject Camera;
    public string key;

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

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            Camera.SetActive(!Camera.activeInHierarchy);
        }
    }
}
