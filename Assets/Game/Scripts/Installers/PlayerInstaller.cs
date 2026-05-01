using Game.Scripts.Feature.Player;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class LocationInstaller : MonoInstaller
    {
        public Transform StartPoint;
        public GameObject PlayerPrefab;
        
        public override void InstallBindings()
        {
            BindPlayer();
        }

        private void BindPlayer()
        {
            Container
                .Bind<Player>()
                .FromComponentInNewPrefab(PlayerPrefab)
                .UnderTransform(StartPoint)
                .AsSingle()
                .NonLazy();
        }
    }
}