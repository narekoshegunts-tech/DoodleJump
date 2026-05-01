using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Scripts.Infrastructure.Input
{
    public class PlayerInputSystem: IInitializable, IDisposable
    {
        [Inject] private InputActionAsset _asset;
        private InputActionMap _playerMap;

        private InputAction _moveAction;

        public Vector2 Move
        {
            get
            {
                if (_moveAction == null)
                {
                    return Vector2.zero;
                }
                return _moveAction.ReadValue<Vector2>();
            }
        }
        
        public void Initialize()
        {
            _playerMap = _asset.FindActionMap("Player");
            _moveAction = _playerMap.FindAction("Move");
            
            _playerMap.Enable();
        }
        
        public void Dispose()
        {
            _playerMap?.Disable();
        }
        
    }
}