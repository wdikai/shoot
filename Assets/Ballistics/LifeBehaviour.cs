using UnityEngine;
using System.Collections;

public class LifeBehaviour : MonoBehaviour {
    public int LifeTime = 10;

    public bool RemoveWhenDie = true;

    private float lifeTime;

    public void Start()
    {
        lifeTime = 0f;
    }

    void FixedUpdate()
    {
        var time = Time.deltaTime;
        lifeTime += time;
        if (lifeTime > LifeTime)
        {
            Die();
        }
    }

    void Die()
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
