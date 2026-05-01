using UnityEngine;

namespace Game.Scripts.Feature.Player.StateMachine.States
{
    public abstract class State
    {
        protected Rigidbody2D _rigidbody;

        public State(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
        }
        public abstract void Enter();
    }
}