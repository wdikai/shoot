namespace Assets.Ballistics.Core
{
    using System;
    using System.Linq;
    using UnityEngine;
    class Pool
    {
        private GameObject[] store;

        public Pool(int size, Func<GameObject> create)
        {
            this.store = new GameObject[size];
            for (var number = 0; number < size; number++)
            {
                this.store[number] = create();
            }
            Debug.LogWarning("Pool created");
        }

        public GameObject Get()
        {
            var bullets = store.Where(go => !go.activeInHierarchy);
            if (bullets.Count() > 0)
            {
                return bullets.First();
            }

            return null;
        }
    }
}