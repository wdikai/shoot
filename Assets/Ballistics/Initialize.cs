using UnityEngine;
using System;
using Assets.Ballistics.Core;

public class Initialize : MonoBehaviour
{
    public GameObject[] ammo;

    public int Size = 10;

    void Start()
    {
        foreach (var bullet in ammo)
        {
            Func<GameObject> BulletFabric = () =>
            {
                var temp = Instantiate(bullet);
                temp.SetActive(false);
                return temp;
            };
            Manager.Instance.AddBullet(bullet.name, Size, BulletFabric);
        }
    }
}
