using UnityEngine;
using Assets.Ballistics.Core;

public class ShotBehavior : MonoBehaviour {

    public AudioClip ShotSound;
    public GameObject BulletSpawner;

    public float Timeout = 1.0f;
    public GameObject Ammo;

    private float timeout;

    void Update () {
        if (Input.GetMouseButtonDown(0) && timeout <= 0)
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

        if (timeout > 0)
        {
            timeout -= Time.deltaTime;
        }

    }
}
