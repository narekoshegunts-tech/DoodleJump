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
        
        private RandomPlatformSpawnStrategy.Factory _spawnStrategyFactory;
        private RandomPlatformSpawnStrategy _spawnStrategy;
        
        private float _downY;
        
        private PlatformPool _platformPool;
        
        [Header("SpawnSettings")]
        [SerializeField] private GameObject _platformPrefab;
        [SerializeField] private float _minDistanceBetweenPlatforms;
        [SerializeField] private float _maxDistanceBetweenPlatforms;
        [SerializeField] private int _capacity;

        [Inject]
        private void Construct(RandomPlatformSpawnStrategy.Factory randomPlatformSpawnStrategyFactory)
        {
            _spawnStrategyFactory = randomPlatformSpawnStrategyFactory;
        }

        private void OnValidate()
        {
            if (_minDistanceBetweenPlatforms > _maxDistanceBetweenPlatforms)
            {
                _maxDistanceBetweenPlatforms = _minDistanceBetweenPlatforms;
            }
        }

        private void Start()
        {
            _downY = _screenBoundService.GetScreenBoundMinPositionY();
            
            float platformWidth = _platformPrefab.GetComponentInChildren<SpriteRenderer>().bounds.size.x;

            _spawnStrategy = _spawnStrategyFactory.Create(_minDistanceBetweenPlatforms,
                _maxDistanceBetweenPlatforms, platformWidth);
            
            Initialize();
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
            platform.transform.position = _spawnStrategy.GetSpawnPosition();
        }

        private void Initialize()
        {
            _platformPool = new PlatformPool(gameObject, _capacity);
            _platformPool.Initialize(_platformPrefab);
            for (int i = 0; i < _capacity; i++)
            {
                if (_platformPool.TryGetObject(out var platform))
                {
                    var spawnPosition = _spawnStrategy.GetSpawnPosition();
                    platform.transform.position = spawnPosition;
                    platform.gameObject.SetActive(true);
                }
            }
        }
    }
}