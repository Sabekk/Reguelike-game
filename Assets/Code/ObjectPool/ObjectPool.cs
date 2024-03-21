using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class ObjectPool : MonoSingleton<ObjectPool>
    {
        #region VARIABLES

        [SerializeField] private List<PoolInstancee> poolInstance;

        private Dictionary<string, PoolInstance> poolDictionary;
        private Dictionary<string, Transform> poolCategory;

        private Transform mainPoolParent;

        #endregion

        #region UNITY_METHODS

        protected override void Awake()
        {
            base.Awake();

            poolDictionary = new Dictionary<string, PoolInstance>();
            poolCategory = new Dictionary<string, Transform>();

            InitializePools();
        }

        #endregion

        #region METHODS

        public PoolObject GetFromPool(string tag)
        {
            PoolInstance instance = null;
            poolDictionary.TryGetValue(tag, out instance);

            if (instance != null)
                return instance.GetFromPool();
            else
            {
                Debug.LogError("Pool cannot be found");
                return null;
            }
        }

        public void ReturnToPool(IPoolable poolableObject)
        {
            PoolObject poolObj = poolableObject.Poolable;
            PoolInstance instance = null;
            poolDictionary.TryGetValue(poolObj.Name, out instance);

            if (instance != null)
                instance.ReturnToPool(poolObj);
            else
            {
                Debug.LogError("Object is not from pool", poolObj.Prefab);
            }
        }

        public void GetAllPoolsOfType(string type, ref List<string> pools)
        {
            foreach (var poolDictionary in poolDictionary)
            {
                if (!poolDictionary.Key.Contains(type))
                    continue;
                pools.Add(poolDictionary.Key);
            }
        }

        private void InitializePools()
        {
            poolDictionary = new Dictionary<string, PoolInstance>();
            foreach (var poolInstance in poolInstance)
            {
                Transform parent = GetCategoryParent(poolInstance.name);
                poolDictionary[poolInstance.name] = new PoolInstance(poolInstance.name, poolInstance.prefab, poolInstance.size, parent);
            }
        }

        private Transform GetCategoryParent(string name)
        {
            if (mainPoolParent == null)
            {
                mainPoolParent = new GameObject("Pools").transform;
                mainPoolParent.SetParent(transform);
            }
            string category = name.Substring(0, name.IndexOf('_'));
            if (poolCategory.ContainsKey(category))
                return poolCategory[category];
            else
            {
                Transform categoryParent = new GameObject(category).transform;
                categoryParent.SetParent(mainPoolParent);
                poolCategory.Add(category, categoryParent);
                return categoryParent;
            }
        }

        #endregion

        #region CLASSES

        public class PoolObject
        {
            #region VARIABLES

            [SerializeField] private string name;
            [SerializeField] private GameObject prefab;
            [SerializeField] private Dictionary<Type, Component> components;
            [SerializeField] private bool taken;

            #endregion

            #region PROPERTIES

            public string Name => name;
            public GameObject Prefab => prefab;
            public bool Taken => taken;

            #endregion

            #region CONSTRUCTORS

            public PoolObject(string name, GameObject prefab)
            {
                this.name = name;
                this.prefab = prefab;

                components = new Dictionary<Type, Component>();
                Component[] componentsTmp = prefab.GetComponents(typeof(Component));
                foreach (var compTmp in componentsTmp)
                {
                    if (compTmp is IPoolable poolableComp)
                        poolableComp.AssignPoolable(this);
                    components[compTmp.GetType()] = compTmp;
                }
            }

            #endregion

            #region METHODS

            public Component GetComponent(Type type)
            {
                Component component = null;
                components.TryGetValue(type, out component);
                if (!component)
                    foreach (var comp in components)
                    {
                        if (type.IsAssignableFrom(comp.Key))
                            return comp.Value;
                    }
                return component;
            }

            public void SetTaken(bool status)
            {
                taken = status;
            }

            public T GetComponent<T>() where T : Component
            {
                return GetComponent(typeof(T)) as T;
            }

            #endregion
        }

        public class PoolInstance
        {
            #region VARIABLES

            private string name;
            private GameObject prefab;
            private Transform parent;
            private Stack<PoolObject> objects;
            private List<PoolObject> taken;

            #endregion

            #region CONSTRUCTORS

            public PoolInstance(string name, GameObject prefab, int initCount, Transform parent)
            {
                this.name = name;
                this.prefab = prefab;
                objects = new Stack<PoolObject>(initCount);
                taken = new List<PoolObject>(initCount);

                this.parent = new GameObject(name).transform;
                this.parent.SetParent(parent);

                for (int i = 0; i < initCount; i++)
                    AddToPool();
            }

            #endregion

            #region METHODS

            public PoolObject GetFromPool()
            {
                if (objects.Count == 0)
                    AddToPool();
                PoolObject poolObject = objects.Pop();
                poolObject.SetTaken(true);
                poolObject.Prefab.SetActive(true);
                taken.Add(poolObject);
                return poolObject;
            }

            public void ReturnToPool(PoolObject poolObject)
            {
                objects.Push(poolObject);
                poolObject.SetTaken(false);
                poolObject.Prefab.SetActive(false);
                poolObject.Prefab.transform.SetParent(parent);

                poolObject.Prefab.transform.position = Vector3.zero;
                poolObject.Prefab.transform.rotation = Quaternion.identity;
                poolObject.Prefab.transform.localScale = Vector3.one;

                taken.Remove(poolObject);
            }

            private void AddToPool()
            {
                GameObject newPoolObject = GameObject.Instantiate(prefab);
                newPoolObject.name = newPoolObject.name + "_Pool";
                ReturnToPool(new PoolObject(name, newPoolObject));
            }

            #endregion
        }

        #endregion

        #region STRUCTS

        [System.Serializable]
        public struct PoolInstancee
        {
            public string name;
            public GameObject prefab;
            public int size;
        }

        #endregion
    }
}