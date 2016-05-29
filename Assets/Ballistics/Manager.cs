using System;
using UnityEngine;

namespace Assets.Ballistics
{
    public class Manager
    {
        private static Manager insance;
        public static Manager Instance
        {
            get
            {
                if (insance == null)
                {
                    insance = new Manager();
                }
                return insance;
            }
        }

        private Pool bulletPool;
        private Manager() {
        }

        public void Initialize(int size, Func<GameObject> bulletFabric) {
            this.bulletPool = new Pool(size, bulletFabric);
            Debug.LogWarning("Manager Initialized");
        }

        public GameObject GetBullet()
        {
            return this.bulletPool.Get();
        }
    }
}
