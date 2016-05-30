using System;
using UnityEngine;

public class ChangeWeaponBehaviour : MonoBehaviour {

    public GameObject[] Weapons;
    public String[] key;

    private int currentItem = 0;

    void Start()
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
            this.Hide(i);
        }
        this.Show(this.currentItem);
    }
    	
	void Update ()
    {
        for (var item = 0; item < this.key.Length; item++)
        {
            if (Input.GetKey(key[item]))
            {
                this.Hide(this.currentItem);
                this.Show(item);
                currentItem = item;
            }
        }
    }

    void Hide(int index)
    {
        if (index >= 0 && index < this.Weapons.Length)
        {
            this.Weapons[index].SetActive(false);
        }
    }

    void Show(int index)
    {
        if (index >= 0 && index < this.Weapons.Length)
        {
            this.Weapons[index].SetActive(true);
        }
    }
}
