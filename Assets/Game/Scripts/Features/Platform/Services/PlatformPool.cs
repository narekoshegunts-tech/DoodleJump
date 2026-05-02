using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Platform.Services
{
    public class PlatformPool: IDisposable
    {
        private GameObject _container;
        private int _capacity;
        
        private List<GameObject> _pool = new List<GameObject>();
        
        // Я знаю это ред флаг но я хз как по другому
        public List<GameObject> Pool => _pool;

        public PlatformPool(GameObject platformPrefab, Transform parent, int capacity)
        {
            _container = new GameObject("PlatformPool");
            _container.transform.SetParent(parent);

            _capacity = capacity;
            
            Initialize(platformPrefab);
        }
        
        private void Initialize(GameObject prefab)
        {
            for (int i = 0; i < _capacity; i++)
            {
                GameObject spawned = UnityEngine.Object.Instantiate(prefab, _container.transform);
                _pool.Add(spawned);
                spawned.SetActive(false);
            }
        }

        public void Dispose()
        {
            _pool.Clear();
        }

        public bool TryGetObject(out GameObject result)
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (!_pool[i].activeSelf)
                {
                    result = _pool[i];
                    return true;
                }
            }

            result = null;
            return false;
        }

        public class Factory : PlaceholderFactory<GameObject, Transform, int, PlatformPool>
        {
        }
    }
}