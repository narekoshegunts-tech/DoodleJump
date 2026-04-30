using UnityEngine;


namespace Gameplay.Platform
{
    public class PlatformSpawner: ObjectPool
    {
        private UnityEngine.Camera _camera;

        private float _rightEdgeX;
        private float _leftEdgeX;

        [SerializeField] private float _minDistanceBetweenPlatforms;
        [SerializeField] private float _maxDistanceBetweenPlatforms;
        private float _lastSpawnPositionY;
        
        [SerializeField] private GameObject _platformPrefab;

        protected override void Awake()
        {
            base.Awake();
            _camera = UnityEngine.Camera.main;

            if (_camera != null)
            {
                _rightEdgeX = _camera.ViewportToWorldPoint(Vector3.right).x;
                _leftEdgeX = _camera.ViewportToWorldPoint(Vector3.zero).x;
                _lastSpawnPositionY = _camera.ViewportToWorldPoint(Vector3.zero).y - _minDistanceBetweenPlatforms;
            }
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            Initialize(_platformPrefab);
            foreach (var item in _pool)
            {
                var spawnPosition = GetSpawnPosition();
                item.transform.position = spawnPosition;
                item.gameObject.SetActive(true);
            }
        }

        private Vector3 GetSpawnPosition()
        {
            float positionX = Random.Range(_leftEdgeX, _rightEdgeX);
            float positionY = Random.Range(_lastSpawnPositionY + _minDistanceBetweenPlatforms, _lastSpawnPositionY + _maxDistanceBetweenPlatforms);
            float positionZ = 0f;
            Vector3 spawnPosition = new Vector3(positionX, positionY, positionZ);
            
            _lastSpawnPositionY = positionY;
            
            return spawnPosition;
        }
    }
}