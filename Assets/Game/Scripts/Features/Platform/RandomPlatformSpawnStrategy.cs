using Game.Scripts.Infrastructure.CameraServices;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Platform
{
    public class RandomPlatformSpawnStrategy
    {
        private ScreenBoundService _screenBoundService;
        
        private float _minDistanceBetweenPlatforms;
        private float _maxDistanceBetweenPlatforms;
        
        private float _rightEdgeX;
        private float _leftEdgeX;
        
        private float _offsetX;
        
        private float _lastSpawnPositionY;

        public RandomPlatformSpawnStrategy(float minDistanceBetweenPlatforms, float maxDistanceBetweenPlatforms, float offsetX)
        {
            _minDistanceBetweenPlatforms = minDistanceBetweenPlatforms;
            _maxDistanceBetweenPlatforms = maxDistanceBetweenPlatforms;
            _offsetX = offsetX;
        }

        [Inject]
        private void Construct(ScreenBoundService screenBoundService)
        {
            _screenBoundService =  screenBoundService;
            
            _lastSpawnPositionY = _screenBoundService.GetScreenBoundMinPositionY();
            
            SetScreenBounds();
        }

        private void SetScreenBounds()
        {
            _rightEdgeX = _screenBoundService.GetScreenBoundMaxPositionX() - _offsetX;
            _leftEdgeX = _screenBoundService.GetScreenBoundMinPositionX() + _offsetX;
        }
        
        public Vector3 GetSpawnPosition()
        {
            SetScreenBounds();
            float positionX = Random.Range(_leftEdgeX, _rightEdgeX);
            float positionY = Random.Range(_lastSpawnPositionY + _minDistanceBetweenPlatforms, _lastSpawnPositionY + _maxDistanceBetweenPlatforms);
            float positionZ = 0f;
            
            Vector3 spawnPosition = new Vector3(positionX, positionY, positionZ);
            
            _lastSpawnPositionY = positionY;
            
            return spawnPosition;
        }

        public class Factory : PlaceholderFactory<float, float, float, RandomPlatformSpawnStrategy>
        {
        }
    }
}