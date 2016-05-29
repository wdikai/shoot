using UnityEngine;
using Assets.Ballistics;

public class ShotBehavior : MonoBehaviour {

    public AudioClip ShotSound;
    public GameObject BulletSpawner;

    public int timeout;
    
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            var bullet = Manager.Instance.GetBullet();
            if (bullet != null)
            {
                var audio = this.GetComponent<AudioSource>();
                if (audio != null)
                {
                    audio.PlayOneShot(ShotSound);
                }

            
                bullet.GetComponent<FlightBehaviour>().init(BulletSpawner.transform);
                bullet.SetActive(true);
            }
        }
	}
}
