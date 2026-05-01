using Game.Scripts.Infrastructure.CameraServices;
using Zenject;

namespace Game.Scripts.Installers
{
    public class CameraServicesInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindScreenBoundService();
        }

        private void BindScreenBoundService()
        {
            Container
                .Bind<ScreenBoundService>()
                .AsSingle()
                .NonLazy();
        }
    }
}