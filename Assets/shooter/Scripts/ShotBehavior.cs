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

    void Update()
    {
        if (timeout <= 0)
        {

            if (Input.GetMouseButtonDown(0) && !HasBurst)
                Shot();
            if (Input.GetMouseButton(0) && HasBurst) Shot();
        }

        if (timeout > 0)
        {
            timeout -= Time.deltaTime;
        }

    }

    void Shot()
    {
        var bullet = Manager.Instance.GetBullet(Ammo.name);
        if (bullet != null)
        {
            var audio = this.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.PlayOneShot(ShotSound);
            }


            bullet.GetComponent<FlightBehaviour>().init(BulletSpawner.transform);
            bullet.SetActive(true);
            timeout = Timeout;
        }
    }
}
