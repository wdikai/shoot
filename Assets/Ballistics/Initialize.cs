using UnityEngine;
using System;
using Assets.Ballistics.Core;

public class Initialize : MonoBehaviour
{
    public GameObject[] Ammo;
    public int[] Sizes;

    private const int Size = 10;

    private void Start()
    {
        for (int i = 0; i < Ammo.Length; i++)
        {
            var size = i < Sizes.Length ? Sizes[i] : Size;
            Func<GameObject> BulletFabric = () =>
            {
                var temp = Instantiate(Ammo[i]);
                temp.SetActive(false);
                return temp;
            };
            Manager.Instance.AddBullet(Ammo[i].name, size, BulletFabric);
        }
    }
}
