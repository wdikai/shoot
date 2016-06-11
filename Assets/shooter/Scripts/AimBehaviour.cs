using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class AimBehaviour : MonoBehaviour
{

    public Texture2D CrossFire;
    public Texture2D CrossFireShadow;

    public GameObject Camera;
    public GameObject Weapon;
    public GameObject Bullet;

    public FirstPersonController FirstPersonController;

    private bool aim = false;
    private Camera mainCamera;
    private MouseLook mouseLook;

    private void Start()
    {
        mainCamera = Camera.GetComponent<Camera>();
        mouseLook = FirstPersonController.m_MouseLook;
        mouseLook.XSensitivity = 1;
        mouseLook.YSensitivity = 1;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (aim) { AimOff(); } else { AimOn(); }
        }
    }

    private void AimOn()
    {
        aim = true;
        if (mainCamera != null)
        {
            mainCamera.depth = 3;
            mainCamera.fieldOfView = 8;
        }
        mouseLook.XSensitivity = 0.1f;
        mouseLook.YSensitivity = 0.1f;
        Weapon.SetActive(false);
        Bullet.transform.position += Vector3.left * -0.05f;
    }

    public void AimOff()
    {
        aim = false;
        if (mainCamera != null)
        {
            mainCamera.depth = 0;
            mainCamera.fieldOfView = 60;
        }
        mouseLook.XSensitivity = 1;
        mouseLook.YSensitivity = 1;
        Weapon.SetActive(true);
        Bullet.transform.position += Vector3.left * 0.05f;
    }

    private void OnGUI()
    {
        if (aim)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 - Screen.height / 2, 0, Screen.height, Screen.height), CrossFire);
            GUI.DrawTexture(new Rect(0, 0, Screen.width / 2 - Screen.height / 2, Screen.height), CrossFireShadow);
            GUI.DrawTexture(new Rect(Screen.width / 2 + Screen.height / 2, 0, Screen.width / 2 - Screen.height / 2, Screen.height), CrossFireShadow);
        }
        else
        {
            GUI.Label(new Rect(Screen.width - 300, Screen.height - 30, 300, 40), "Press right mouse button for toggle AIM mode");
        }
    }
}
