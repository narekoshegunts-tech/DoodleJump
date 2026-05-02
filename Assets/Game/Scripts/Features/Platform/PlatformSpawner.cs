using Game.Scripts.Features.Platform.Services;
using Game.Scripts.Infrastructure.CameraServices;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Platform
{
    public class PlatformSpawner: MonoBehaviour
    {
        [Inject] private ScreenBoundService _screenBoundService;
        
        private RandomPlatformSpawnStrategy.Factory _spawnStrategyFactory;
        private RandomPlatformSpawnStrategy _spawnStrategy;
        
        private PlatformRecyclerService.Factory _recyclerServiceFactory;
        private PlatformRecyclerService _recyclerService;
        
        private PlatformPool _platformPool;
        
        [Header("SpawnSettings")]
        [SerializeField] private GameObject _platformPrefab;
        [SerializeField] private float _minDistanceBetweenPlatforms;
        [SerializeField] private float _maxDistanceBetweenPlatforms;
        [SerializeField] private int _capacity;

        
        [Inject]
        private void Construct(RandomPlatformSpawnStrategy.Factory randomPlatformSpawnStrategyFactory, 
            PlatformPool.Factory platformPoolFactory, PlatformRecyclerService.Factory recyclerServiceFactory)
        {
            _spawnStrategyFactory = randomPlatformSpawnStrategyFactory;
            _recyclerServiceFactory = recyclerServiceFactory;
            
            _platformPool = platformPoolFactory.Create(_platformPrefab, transform, _capacity);
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
            float platformWidth = _platformPrefab.GetComponentInChildren<SpriteRenderer>().bounds.size.x;

            _spawnStrategy = _spawnStrategyFactory.Create(_minDistanceBetweenPlatforms,
                _maxDistanceBetweenPlatforms, platformWidth);
            
            Initialize();

            _recyclerService = _recyclerServiceFactory.Create(_platformPool.Pool, _spawnStrategy);
        }

        private void Update()
        {
            _recyclerService.Update();
        }
        

        private void Initialize()
        {
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