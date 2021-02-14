using UnityEngine;
using System.Collections.Generic;

namespace Core.Utilities
{
    public class Pool<T> where T : Component
    {
        public enum PoolExpandMethods
        {
            Single,
            Double
        }

        private int poolSize;
        private T poolObject;
        private Transform parentTransform;
        private Stack<T> poolStack;
        private List<T> spawnedObjects;
        private PoolExpandMethods expandMethod;

        public List<T> SpawnedObjects { get => spawnedObjects; }

        /// <summary>
        /// Pool system dedicated for Unity Component
        /// </summary>
        /// <param name="poolObject">Poolable object that will be spawned by the pool</param>
        /// <param name="parentTransform">Parent transform for poolObjects</param>
        /// <param name="initialSize">Initial max size of the pool</param>
        /// <param name="expandMethod">Possible methods how to handle pool expanding</param>
        public Pool(T poolObject, Transform parentTransform = null, int initialSize = 10, PoolExpandMethods expandMethod = PoolExpandMethods.Double)
        {
            this.poolSize = initialSize;
            this.poolStack = new Stack<T>(poolSize);
            this.spawnedObjects = new List<T>(poolSize);
            this.parentTransform = parentTransform;
            this.poolObject = poolObject;
            this.expandMethod = expandMethod;

            for (int i = 0; i < poolSize; i++)
            {
                poolStack.Push(CreateObject());
            }
        }

        public T Spawn()
        {
            return Spawn(Vector3.zero);
        }

        public T Spawn(Vector3 position)
        {
            return Spawn(position, Quaternion.identity);
        }

        public T Spawn(Vector3 position, Quaternion rotation)
        {
            return Spawn(position, Quaternion.identity, parentTransform);
        }

        public T Spawn(Vector3 position, Quaternion rotation, Transform parent)
        {
            if (poolStack.Count == 0)
            {
                ExpandPool();
            }

            var item = poolStack.Pop();
            item.transform.SetParent(parent);
            item.gameObject.SetActive(true);
            item.transform.position = position;
            item.transform.rotation = rotation;

            spawnedObjects.Add(item);

            return item.GetComponent<T>();
        }

        public void ReturnToPool(T item)
        {
            //prevents from returning the same item twice
            if (poolStack.Contains(item))
                return;
            
            //remove item from the spawned objects list if possible
            bool doesPoolCointainTheItem = spawnedObjects.Remove(item);

            //prevents from returning the item that wasn't spawned by this pool
            if (!doesPoolCointainTheItem)
                return;

            poolStack.Push(item);
            item.transform.SetParent(parentTransform);
            item.gameObject.SetActive(false);
        }

        public void ClearPool()
        {
            for (int i = spawnedObjects.Count-1; i >= 0; i--)
            {
                ReturnToPool(spawnedObjects[i]);
            }
        }

        private void ExpandPool()
        {
            switch (expandMethod)
            {
                case PoolExpandMethods.Single:
                    {
                        Resize(poolSize + 1);
                        break;
                    }
                case PoolExpandMethods.Double:
                    {
                        if (poolSize == 0)
                        {
                            Resize(1);
                        }
                        else
                        {
                            Resize(poolSize * 2);
                        }
                        break;
                    }
                default:
                    break;
            }
            Debug.LogWarning($"{typeof(T)} pool expanded using {expandMethod} method. New size = {poolSize}");
        }

        private void Resize(int desiredPoolSize)
        {
            if (poolSize == desiredPoolSize) return;

            while (desiredPoolSize < poolStack.Count)
            {
                Object.Destroy(poolStack.Pop());
            }

            while (desiredPoolSize > poolStack.Count)
            {
                poolStack.Push(CreateObject());
            }

            poolSize = desiredPoolSize;
        }

        private T CreateObject()
        {
            var newItem = GameObject.Instantiate(poolObject, parentTransform);
            newItem.gameObject.SetActive(false);

            return newItem;
        }
    }
}
