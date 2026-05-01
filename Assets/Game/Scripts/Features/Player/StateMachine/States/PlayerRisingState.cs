using UnityEngine;

namespace Game.Scripts.Features.Player.StateMachine.States
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
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}