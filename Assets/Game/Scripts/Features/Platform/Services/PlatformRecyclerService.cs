using System.Collections.Generic;
using Game.Scripts.Infrastructure.CameraServices;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Platform.Services
{
    public class PlatformRecyclerService
    {
        private List<GameObject> _platformPool;
        [Inject] private ScreenBoundService _screenBoundService;
        
        private RandomPlatformSpawnStrategy _spawnStrategy;

        private float _downY;

        public PlatformRecyclerService(List<GameObject> platformPool, RandomPlatformSpawnStrategy spawnStrategy)
        {
            _platformPool = platformPool;
            _spawnStrategy = spawnStrategy;
        }
        
        public void Update()
        {
            RecyclePlatformsBelowScreen();
        }
        
        private void RecyclePlatformsBelowScreen()
        {
            _downY = _screenBoundService.GetScreenBoundMinPositionY();
            foreach (var item in _platformPool)
            {
                if (item.transform.position.y < _downY)
                    MovePlatformUp(item);
            }
        }
        
        private void MovePlatformUp(GameObject platform)
        {
            platform.transform.position = _spawnStrategy.GetSpawnPosition();
        }

        public class Factory : PlaceholderFactory<List<GameObject>, RandomPlatformSpawnStrategy,
            PlatformRecyclerService>
        {
        }
    }
}