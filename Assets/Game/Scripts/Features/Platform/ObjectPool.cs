using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.Feature.Platform
{
    public class ObjectPool: MonoBehaviour
    {
        private GameObject _container;
        [SerializeField] private int _capacity;
        
        protected List<GameObject> _pool = new List<GameObject>();

        protected virtual void Awake()
        {
            _container = new GameObject("ObjectPoolContainer");
            _container.transform.SetParent(transform);
        }

        protected void Initialize(GameObject prefab)
        {
            for (int i = 0; i < _capacity; i++)
            {
                GameObject spawned = Instantiate(prefab, _container.transform);
                _pool.Add(spawned);
                spawned.SetActive(false);
            }
        }

        protected bool TryGetObject(out GameObject result)
        {
            result = _pool.FirstOrDefault(p => p.activeSelf == false);
            return result != null;
        }
    }
}