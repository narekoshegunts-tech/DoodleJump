using Game.Scripts.Features.Common;
using Game.Scripts.UI;
using Game.Scripts.UI.Services.Signals;
using Zenject;

namespace Game.Scripts.Installers
{
    public class CommonInstallers: MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallSignalBus();
            DeclareSignals();
            BindPauseService();
            BindEndGameUI();
            BindGameFlowController();
        }

        public void DeclareSignals()
        {
            Container.DeclareSignal<PlayerDiedSignal>();
            Container.DeclareSignal<RestartRequestSignal>();
        }

        private void InstallSignalBus()
        {
            SignalBusInstaller.Install(Container);
        }

        private void BindEndGameUI()
        {
            Container
                .Bind<EndGame>()
                .FromComponentInHierarchy()
                .AsSingle();
        }

        private void BindPauseService()
        {
            Container
                .Bind<GamePauseService>()
                .AsSingle();
        }

        private void BindGameFlowController()
        {
            Container
                .BindInterfacesAndSelfTo<GameFlowController>()
                .AsSingle()
                .NonLazy();
        }
    }
}