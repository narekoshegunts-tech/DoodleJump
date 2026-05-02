using Game.Scripts.Features.Player;
using Game.Scripts.Features.Player.Services;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private PlayerDeathDetector _playerPrefab;
        
        
        public override void InstallBindings()
        {
            BindPlayer();
            BindPlayerServicesFactories();
        }

        private void BindPlayerServicesFactories()
        {
            Container
                .BindFactory<Rigidbody2D, Transform, float, MovementService,  MovementService.Factory>();
            
            Container
                .BindFactory<Rigidbody2D, float, JumpService, JumpService.Factory>();

            Container
                .BindFactory<Transform, DeathDetectorService, DeathDetectorService.Factory>();
        }

        private void BindPlayer()
        { 
            Container
                .Bind<PlayerDeathDetector>()
                .FromComponentInNewPrefab(_playerPrefab)
                .UnderTransform(_startPoint)
                .AsSingle()
                .NonLazy();
        }
    }
}