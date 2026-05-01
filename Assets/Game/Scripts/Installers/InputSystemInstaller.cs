using Zenject;

namespace Game.Scripts.Installers
{
    public class InputSystemInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInputSystem();
        }

        private IfNotBoundBinder BindInputSystem()
        {
            return Container.Bind<InputSystem>()
                .AsSingle()
                .NonLazy();
        }
    }
}