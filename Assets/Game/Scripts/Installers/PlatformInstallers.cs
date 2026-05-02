using Game.Scripts.Features.Platform;
using Zenject;

namespace Game.Scripts.Installers
{
    public class PlatformInstallers: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPlatformRandomSpawnStrategyFactory();
        }

        private void BindPlatformRandomSpawnStrategyFactory()
        {
            Container
                .BindFactory<float, float, float, RandomPlatformSpawnStrategy, RandomPlatformSpawnStrategy.Factory>();
        }
    }
}