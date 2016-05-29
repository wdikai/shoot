namespace Assets.Ballistics
{
    using System;
    using System.Linq;
    using UnityEngine;
    class Pool
    {
        private GameObject[] store;
        private int size;

        public Pool(int size, Func<GameObject> create)
        {
            this.size = size;
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