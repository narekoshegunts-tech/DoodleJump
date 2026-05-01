using System.ComponentModel;
using Game.Scripts.Features.Player;
using Game.Scripts.Features.Player.Services;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private PlayerController _playerPrefab;
        
        
        public override void InstallBindings()
        {
            BindPlayer();
            BindPlayerServices();
        }

        private void BindPlayerServices()
        {
            Container
                .Bind<MovementService>()
                .AsTransient();
            
            Container
                .Bind<JumpService>()
                .AsTransient();

            Container
                .Bind<DeathDetectorService>()
                .AsTransient();
        }

        private void BindPlayer()
        { 
            Container
                .Bind<PlayerController>()
                .FromComponentInNewPrefab(_playerPrefab)
                .UnderTransform(_startPoint)
                .AsSingle()
                .NonLazy();
        }
    }
}