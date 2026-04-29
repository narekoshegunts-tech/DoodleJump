using Gameplay.Player.StateMachine.States;
using UnityEngine;

namespace Gameplay.Player.StateMachine
{
    public class PlayerStateMachine: MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        
        private State _currentState;

        private State _risingState;
        private State _fallingState;
        private State _deathState;
        
        [SerializeField] private float _jumpForce;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            
            _risingState = new PlayerRisingState(_rigidbody2D, _jumpForce);
            _fallingState = new PlayerFallingState(_rigidbody2D);
            _deathState = new PlayerDeathState(_rigidbody2D);
        }

        private void Update()
        {
            if (_currentState is PlayerRisingState && _rigidbody2D.linearVelocity.y < 0)
            {
                ChangeState(_fallingState);
            }
        }

        private void Start()
        {
            ChangeState(_fallingState);
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