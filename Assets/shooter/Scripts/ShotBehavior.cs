using UnityEngine;
using Assets.Ballistics.Core;

public class ShotBehavior : MonoBehaviour
{
    public AudioClip ShotSound;
    public GameObject BulletSpawner;

    public bool HasBurst = false;

    public float Timeout = 1.0f;
    public GameObject Ammo;

    private float timeout;

    private bool hasGravity = true;
    private bool hasAtmosphere= true;
    private bool hasWind= true;

    private void Update()
    {
        if (timeout <= 0)
        {

            if (Input.GetMouseButtonDown(0) && !HasBurst)
                Shot();
            if (Input.GetMouseButton(0) && HasBurst) Shot();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            hasGravity = !hasGravity;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            hasAtmosphere = !hasAtmosphere;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            hasWind = !hasWind;
        }

        if (timeout > 0)
        {
            timeout -= Time.deltaTime;
        }

    }

    private void Shot()
    {
        var bullet = Manager.Instance.GetBullet(Ammo.name);
        if (bullet != null)
        {
            var audio = this.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.PlayOneShot(ShotSound);
            }
            
            bullet.GetComponent<FlightBehaviour>().Init(BulletSpawner.transform);

            var grph = GetComponent<GraphicBehaviour>();
            if (grph != null)
            {
                grph.Bullet = bullet;
                grph.Init();
            }

            bullet.SetActive(true);
            SetInteraction(bullet);
            timeout = Timeout;
        }
    }

    private void SetInteraction(GameObject bullet)
    {
        var gravity = bullet.GetComponent<GravityBehaviour>();
        var atmosphere = bullet.GetComponent<AtmosphereBehaviour>();
        var wind = bullet.GetComponent<WindBehaviour>();

        if (gravity != null)
        {
            Debug.Log("Gravity :" + (hasGravity ? "ON" : "OFF"));
            gravity.Enable = hasGravity;
        }
        if (atmosphere != null)
        {
            Debug.Log("Atmosphere :" + (hasAtmosphere ? "ON" : "OFF"));
            atmosphere.Enable = hasAtmosphere;
        }
        if (wind != null)
        {
            Debug.Log("Wind :" + (hasWind ? "ON" : "OFF"));
            wind.Enable = hasWind;
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width - 200, 10, 200, 40), "(press T for changing) Gravity :" + (hasGravity ? "ON" : "OFF"));
        GUI.Label(new Rect(Screen.width - 200, 50, 200, 40), "(press Y for changing) Atmosphere :" + (hasAtmosphere ? "ON" : "OFF"));
        GUI.Label(new Rect(Screen.width - 200, 90, 200, 40), "(press U for changing) Wind :" + (hasWind? "ON" : "OFF"));
    }
}
