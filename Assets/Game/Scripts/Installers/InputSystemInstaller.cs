using Game.Scripts.Infrastructure.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Scripts.Installers
{
    public class InputSystemInstaller: MonoInstaller
    {
        [SerializeField] private InputActionAsset _inputActions;
        public override void InstallBindings()
        {
            BindInputSystem();
        }

        private void BindInputSystem()
        {
            Container.Bind<InputActionAsset>()
                .FromInstance(_inputActions)
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<PlayerInputSystem>()
                .AsSingle()
                .NonLazy();
        }
    }
}