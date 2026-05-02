using System;
using Game.Scripts.Features.Player.Services;
using Game.Scripts.UI.Services.Signals;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController: MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpForce;
        
        private MovementService _movementService;
        private JumpService _jumpService;
        private DeathDetectorService _deathDetectorService;
        
        private Rigidbody2D _rigidbody2D;

        public event Action<float> OnDirectionChanged;
        
        private bool _isDead = false;
        
        private bool _isFalling => _rigidbody2D.velocity.y < 0;
        
        [Inject]
        private void Construct(MovementService movementService, JumpService jumpService, DeathDetectorService deathDetectorServiceService)
        {
            _movementService = movementService;
            _jumpService = jumpService;
            _deathDetectorService = deathDetectorServiceService;
        }
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            InitMovementService();
            InitJumpService();
            InitDeathDetectorService();
        }

        private void InitDeathDetectorService()
        {
            _deathDetectorService.SetProperties(transform);
        }

        private void InitMovementService()
        {
            _movementService.SetProperties(transform, _rigidbody2D, _moveSpeed);
        }
        
        private void InitJumpService()
        {
            _jumpService.SetProperties(_rigidbody2D, _jumpForce);
        }
        
        private void Update()
        {
            if (_isDead)
                return;
            
            _movementService.Update();
            if (_isFalling)
            {
                _deathDetectorService.Update();
                return;
            }
            // при падении нижнюю границу не считаем
            _deathDetectorService.SetMinPosition();
        }
        
        private void ChangeDirection(float direction)
        {
            OnDirectionChanged?.Invoke(direction);
        }

        private void OnEnable()
        {
            _movementService.OnDirectionChanged += ChangeDirection;
            _deathDetectorService.OnDeath += Die;
        }

        private void OnDisable()
        {
            _movementService.OnDirectionChanged -= ChangeDirection;
            _deathDetectorService.OnDeath -= Die;
        }

        private void Die()
        {
            _isDead = true;
            _signalBus.Fire<PlayerDiedSignal>();
        }

        public void CollidedWithPlatform()
        {
            _jumpService.CollidedWithPlatform();
        }

    }
}