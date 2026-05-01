using System;
using Game.Scripts.Feature.Player.StateMachine;
using UnityEngine;

namespace Game.Scripts.Feature.Player
{
    public class Player: MonoBehaviour
    {
        PlayerStateMachine _playerStateMachine;
        Rigidbody2D _rigidbody2D;
        
        [SerializeField] private float _jumpForce;

        public event Action OnDie;
        

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _playerStateMachine = new PlayerStateMachine(_rigidbody2D, transform, _jumpForce);
        }

        private void Update()
        {
            _playerStateMachine.Update();
        }

        private void OnEnable()
        {
            _playerStateMachine.OnDie += Die;
        }

        private void OnDisable()
        {
            _playerStateMachine.OnDie -= Die;
        }

        public void CollidedWithPlatform()
        {
            _playerStateMachine.CollidedWithPlatform();
        }

        private void Die()
        {
            OnDie?.Invoke();
        }
    }
}