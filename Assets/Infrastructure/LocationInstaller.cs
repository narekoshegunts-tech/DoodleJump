using Gameplay.Camera;
using UnityEngine;
using Zenject;
using Gameplay.Player;

namespace Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        public Transform StartPoint;
        public GameObject PlayerPrefab;
        // ReSharper disable Unity.PerformanceAnalysis
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