using UnityEngine;
using System;
using Assets.Ballistics;

public class Initialize : MonoBehaviour {

    public GameObject bullet;
    public int Size = 10;

	void Start () {
        Func<GameObject> BulletFabric = () => {
            var temp = Instantiate(bullet);
            temp.SetActive(false);
            return temp;
        };
        Manager.Instance.Initialize(Size, BulletFabric);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
