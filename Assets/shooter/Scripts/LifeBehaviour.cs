using UnityEngine;
using System.Collections;

public class LifeBehaviour : MonoBehaviour {
    public int LifeTime = 10;

    public bool RemoveWhenDie = true;

    private float lifeTime;

    private void Start()
    {
        lifeTime = 0f;
    }

    private void FixedUpdate()
    {
        var time = Time.deltaTime;
        lifeTime += time;
        if (lifeTime > LifeTime)
        {
            Die();
        }
    }

    private void Die()
    {
        if (this.RemoveWhenDie)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
