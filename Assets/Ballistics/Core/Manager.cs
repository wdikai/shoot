namespace Assets.Ballistics.Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

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
        
        private Dictionary<string, Pool> bulletPools;

        private Manager()
        {
            this.bulletPools = new Dictionary<string, Pool>();
        }

        public void AddBullet(string name, int count, Func<GameObject> bulletFabric)
        {
            var bulletPool = new Pool(count, bulletFabric);
            this.bulletPools.Add(name, bulletPool);
        }

        public GameObject GetBullet(string name)
        {
            return this.bulletPools[name].Get();
        }
    }
}
