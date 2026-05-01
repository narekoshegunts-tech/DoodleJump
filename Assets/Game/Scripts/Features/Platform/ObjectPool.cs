using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.Features.Platform
{
    public class ObjectPool
    {
        private GameObject _container;
        private int _capacity;
        
        private List<GameObject> _pool = new List<GameObject>();

        public ObjectPool(GameObject parent, int capacity)
        {
            _capacity = capacity;
            
            _container = new GameObject("ObjectPoolContainer");
            _container.transform.SetParent(parent.transform);
        }

        public void Initialize(GameObject prefab)
        {
            for (int i = 0; i < _capacity; i++)
            {
                GameObject spawned = Object.Instantiate(prefab, _container.transform);
                _pool.Add(spawned);
                spawned.SetActive(false);
            }
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

        public List<GameObject> ReturnObjectsLowerPosition(float position)
        {
            List<GameObject> result = new List<GameObject>();
            foreach (var item in _pool)
            {
                if (item.transform.position.y <= position)
                {
                    result.Add(item);
                }
            }

            return result;
        }
        
    }
}