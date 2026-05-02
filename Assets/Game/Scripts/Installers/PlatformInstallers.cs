using System.Collections.Generic;
using Game.Scripts.Features.Platform.Services;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class PlatformInstallers: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPlatformRandomSpawnStrategyFactory();
            BindPlatformPoolFactory();
            BindPlatformRecyclerService();
        }

        private void BindPlatformRecyclerService()
        {
            Container
                .BindFactory<List<GameObject>, RandomPlatformSpawnStrategy, PlatformRecyclerService, PlatformRecyclerService.Factory>();
        }

        private void BindPlatformPoolFactory()
        {
            Container
                .BindFactory<GameObject, Transform, int, PlatformPool, PlatformPool.Factory>();
        }

        private void BindPlatformRandomSpawnStrategyFactory()
        {
            Container
                .BindFactory<float, float, float, RandomPlatformSpawnStrategy, RandomPlatformSpawnStrategy.Factory>();
        }
    }
}