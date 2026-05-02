using System;
using Game.Scripts.Infrastructure.CameraServices;
using Game.Scripts.Infrastructure.Input;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player.Services
{
    public class MovementService
    {
        private ScreenBoundService _screenBoundService;
        
        private PlayerInputSystem _input;
        
        private Transform _transform;
        private Rigidbody2D _rigidbody2D;

        private float _speed;
        private float _minX;
        private float _maxX;

        public event Action<float> OnDirectionChanged;

        private float _lastDirection;

        public MovementService(Rigidbody2D rigidbody2D,Transform transform, float speed)
        {
            _transform = transform;
            _rigidbody2D = rigidbody2D;
            _speed = speed;
        }

        [Inject]
        public void Construct(PlayerInputSystem input, ScreenBoundService screenBoundService)
        {
            _input = input;
            _screenBoundService = screenBoundService;
            SetScreenBounds();
        }

        private void SetScreenBounds()
        {
            _minX = _screenBoundService.GetScreenBoundMinPositionX();
            _maxX = _screenBoundService.GetScreenBoundMaxPositionX();
        }

        public void FixedUpdate()
        {
            CheckChangedDirection();
            Move();
        }

        private void CheckChangedDirection()
        {
            var direction = _input.Move.x;
            if ((direction > 0 && _lastDirection <= 0) || 
                (direction < 0 && _lastDirection >= 0))
            {
                ChangeDirection(direction);
            }

            _lastDirection = direction;
        }

        private void Move()
        {
            _rigidbody2D.velocity = new Vector2(_speed * _lastDirection, _rigidbody2D.velocity.y);
            
            if (_transform.position.x > _maxX)
            {
                _transform.position = new Vector2(_maxX, _transform.position.y);
            }
            else if (_transform.position.x < _minX)
            {
                _transform.position = new Vector2(_minX, _transform.position.y);
            }
        }

        private void ChangeDirection(float direction)
        {
            OnDirectionChanged?.Invoke(direction);
        }

        public class Factory : PlaceholderFactory<Rigidbody2D, Transform, float, MovementService>
        {
        }
    }
}