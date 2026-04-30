using Gameplay.Player.StateMachine.States;
using UnityEngine;
using System;

namespace Gameplay.Player.StateMachine
{
    public class PlayerStateMachine
    {
        private Rigidbody2D _rigidbody2D;
        private Transform _transform;
        
        private State _currentState;

        private State _risingState;
        private State _fallingState;
        private State _deathState;
        
        private float _jumpForce;

        public event Action OnDie;

        public PlayerStateMachine(Rigidbody2D rigidbody2D, Transform transform, float jumpForce)
        {
            _rigidbody2D = rigidbody2D;
            _transform = transform;
            _jumpForce = jumpForce;
            
            _risingState = new PlayerRisingState(_rigidbody2D, _jumpForce);
            _fallingState = new PlayerFallingState(_rigidbody2D);
            _deathState = new PlayerDeathState(_rigidbody2D);
            
            ChangeState(_fallingState);
        }

        public void Update()
        {
            if (_currentState is PlayerRisingState && _rigidbody2D.linearVelocity.y < 0)
            {
                ChangeState(_fallingState);
            }
            else if (_currentState is PlayerFallingState &&
                     UnityEngine.Camera.main.WorldToViewportPoint(_transform.position).y < 0)
            {
                ChangeState(_deathState);
                OnDie?.Invoke();
            }
        }

        public void CollidedWithPlatform()
        {
            if (_currentState is PlayerFallingState)
            {
                ChangeState(_risingState);
            }
        }

        private void ChangeState(State newState)
        {
            _currentState = newState;
            _currentState.Enter();
        }
    }
}