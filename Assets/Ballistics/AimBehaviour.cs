using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

public class AimBehaviour : MonoBehaviour {

    public Texture2D CrossFire;
    public GameObject Camera;
    public FirstPersonController FirstPersonController;

    private bool aim = false;
    private Camera mainCamera;
    private MouseLook mouseLook;

    void Start ()
    {
        mainCamera = Camera.GetComponent<Camera>();
        mouseLook = FirstPersonController.m_MouseLook;
    }

    void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            if (aim) { AimOff(); } else { AimOn(); }
        }
    }

    void AimOn()
    {
        aim = true;
        if (mainCamera != null)
        {
            mainCamera.depth = 2;
            mainCamera.fieldOfView = 8;
        }
        mouseLook.XSensitivity = 0.1f;
        mouseLook.YSensitivity = 0.1f;
    }

    void AimOff()
    {
        aim = false;
        if (mainCamera != null)
        {
            mainCamera.depth = 0;
            mainCamera.fieldOfView = 60;
        }
        mouseLook.XSensitivity = 10;
        mouseLook.YSensitivity = 10;
    }

    void OnGUI()
    {
        if (aim)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), CrossFire);
        }
    }
}
