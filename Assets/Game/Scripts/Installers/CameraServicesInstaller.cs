using Game.Scripts.Infrastructure.CameraServices;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class CameraServicesInstaller: MonoInstaller
    {
        [SerializeField] private Camera _camera;
        public override void InstallBindings()
        {
            BindCamera();
            BindScreenBoundService();
        }

        private void BindCamera()
        {
            Container
                .Bind<Camera>()
                .FromInstance(_camera)
                .AsSingle();
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