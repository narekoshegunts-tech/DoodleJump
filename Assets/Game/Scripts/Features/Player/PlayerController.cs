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
        [SerializeField] private float _jumpForce;
        
        private JumpService _jumpService;
        private DeathDetectorService _deathDetectorService;
        
        private Rigidbody2D _rigidbody2D;
        
        private bool _isDead = false;
        
        private bool _isFalling => _rigidbody2D.velocity.y < 0;
        
        [Inject]
        private void Construct(JumpService jumpService, DeathDetectorService deathDetectorServiceService)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            
            _jumpService = jumpService;
            _deathDetectorService = deathDetectorServiceService;
        }

        private void Start()
        {
            InitJumpService();
            InitDeathDetectorService();
        }

        private void InitDeathDetectorService()
        {
            _deathDetectorService.SetProperties(transform);
        }
        
        private void InitJumpService()
        {
            _jumpService.SetProperties(_rigidbody2D, _jumpForce);
        }
        
        private void Update()
        {
            if (_isDead)
                return;
            
            if (_isFalling)
            {
                _deathDetectorService.Update();
                return;
            }
            // при падении нижнюю границу не считаем
            _deathDetectorService.SetMinPosition();
        }
        
        private void OnEnable()
        {
            _deathDetectorService.OnDeath += Die;
        }

        private void OnDisable()
        {
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