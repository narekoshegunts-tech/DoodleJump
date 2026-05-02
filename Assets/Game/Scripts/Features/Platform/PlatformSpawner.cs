using System;
using Game.Scripts.Infrastructure.CameraServices;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Scripts.Features.Platform
{
    public class PlatformSpawner: MonoBehaviour
    {
        [Inject] private ScreenBoundService _screenBoundService;

        private float _rightEdgeX;
        private float _leftEdgeX;
        private float _downY;
        
        private float _lastSpawnPositionY;
        
        private PlatformPool _platformPool;
        
        [Header("SpawnSettings")]
        [SerializeField] private GameObject _platformPrefab;
        [SerializeField] private float _minDistanceBetweenPlatforms;
        [SerializeField] private float _maxDistanceBetweenPlatforms;
        [SerializeField] private int _capacity;

        private float _platformWidth;

        private void OnValidate()
        {
            if (_minDistanceBetweenPlatforms > _maxDistanceBetweenPlatforms)
            {
                _maxDistanceBetweenPlatforms = _minDistanceBetweenPlatforms;
            }
        }

        private void Start()
        {
            SetScreenBounds();
            
            _downY = _screenBoundService.GetScreenBoundMinPositionY();
            _lastSpawnPositionY = _downY - _minDistanceBetweenPlatforms;
            
            _platformWidth = _platformPrefab.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
            
            Initialize();
        }

        private void SetScreenBounds()
        {
            _rightEdgeX = _screenBoundService.GetScreenBoundMaxPositionX();
            _leftEdgeX = _screenBoundService.GetScreenBoundMinPositionX();
        }

        private void Update()
        {
            RecyclePlatformsBelowScreen();
        }

        private void RecyclePlatformsBelowScreen()
        {
            _downY = _screenBoundService.GetScreenBoundMinPositionY();
            foreach (var item in _platformPool.ReturnObjectsLowerPosition(_downY))
            {
                MovePlatformUp(item);
            }
        }

        private void MovePlatformUp(GameObject platform)
        {
            platform.transform.position = GetSpawnPosition();
        }

        private void Initialize()
        {
            _platformPool = new PlatformPool(gameObject, _capacity);
            _platformPool.Initialize(_platformPrefab);
            for (int i = 0; i < _capacity; i++)
            {
                if (_platformPool.TryGetObject(out var platform))
                {
                    var spawnPosition = GetSpawnPosition();
                    platform.transform.position = spawnPosition;
                    platform.gameObject.SetActive(true);
                }
            }
        }

        private Vector3 GetSpawnPosition()
        {
            SetScreenBounds();
            float positionX = Random.Range(_leftEdgeX + _platformWidth / 2, _rightEdgeX - _platformWidth / 2);
            float positionY = Random.Range(_lastSpawnPositionY + _minDistanceBetweenPlatforms, _lastSpawnPositionY + _maxDistanceBetweenPlatforms);
            float positionZ = 0f;
            Vector3 spawnPosition = new Vector3(positionX, positionY, positionZ);
            
            _lastSpawnPositionY = positionY;
            
            return spawnPosition;
        }
    }
}