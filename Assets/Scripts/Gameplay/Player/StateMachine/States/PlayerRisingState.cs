
using UnityEngine;

namespace Gameplay.Player.StateMachine.States
{
    public class PlayerRisingState: State
    {
        private float _jumpForce;
        public PlayerRisingState(Rigidbody2D rigidbody, float force) : base(rigidbody)
        {
            _jumpForce = force;
        }

        public override void Enter()
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}