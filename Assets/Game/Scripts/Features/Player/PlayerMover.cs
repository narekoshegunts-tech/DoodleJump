using System;
using Game.Scripts.Features.Player.Services;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerMover:MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        
        private MovementService _movementService;
        [SerializeField] private float _moveSpeed;
        
        public event Action<float> OnDirectionChanged;
        
        [Inject]
        private void Construct(MovementService.Factory movementServiceFactory)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            
            _movementService = movementServiceFactory.Create(_rigidbody2D, transform, _moveSpeed);
        }

        private void FixedUpdate()
        {
            _movementService.FixedUpdate();
        }

        private void OnEnable()
        {
            _movementService.OnDirectionChanged += ChangeDirection;
        }

        private void OnDisable()
        {
            _movementService.OnDirectionChanged -= ChangeDirection;
        }
        
        private void ChangeDirection(float direction)
        {
            OnDirectionChanged?.Invoke(direction);
        }
    }
}