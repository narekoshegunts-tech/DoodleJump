using System;
using Game.Scripts.Infrastructure.CameraServices;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player.Services
{
    public class DeathDetectorService
    {
        private Transform _transform; 
        private ScreenBoundService _screenBoundService;
        private float _minPositionY;

        public event Action OnDeath;

        private bool _isDead;

        public DeathDetectorService(Transform transform)
        {
            _transform = transform;
        }

        [Inject]
        private void Construct(ScreenBoundService screenBoundService)
        {
            _screenBoundService = screenBoundService;
            SetMinPosition();
        }

        public void SetMinPosition()
        {
            _minPositionY = _screenBoundService.GetScreenBoundMinPositionY();
        }

        public void Update()
        {
            if (_isDead)
            {
                return;
            }
            if (_transform?.position.y < _minPositionY)
            {
                Die();
            }
        }

        private void Die()
        {
            _isDead = true;
            OnDeath?.Invoke();
        }

        public class Factory : PlaceholderFactory<Transform, DeathDetectorService>
        {
        }
        
    }
}