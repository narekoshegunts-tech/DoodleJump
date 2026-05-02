using Game.Scripts.UI;
using Game.Scripts.UI.Services;
using Zenject;

namespace Game.Scripts.Installers
{
    public class UIInstallers: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindScoreCounter();
            BindEndGameUI();
        }

        private void BindScoreCounter()
        {
            Container
                .Bind<ScoreCounter>()
                .AsSingle();
        }

        private void BindEndGameUI()
        {
            Container
                .Bind<EndGame>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}