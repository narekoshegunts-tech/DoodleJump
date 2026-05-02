using Game.Scripts.Features.Player.Services;
using Game.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerDeathDetector: MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;
        
        private DeathDetectorService _deathDetectorService;
        
        private Rigidbody2D _rigidbody2D;
        
        private bool _isFalling => _rigidbody2D.velocity.y < 0;
        
        [Inject]
        private void Construct(DeathDetectorService.Factory deathDetectorServiceServiceFactory)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _deathDetectorService = deathDetectorServiceServiceFactory.Create(transform);
        }
        
        private void Update()
        {
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
            _signalBus.Fire<PlayerDiedSignal>();
        }

    }
}