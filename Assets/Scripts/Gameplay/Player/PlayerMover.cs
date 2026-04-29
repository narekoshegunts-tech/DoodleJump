using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerMover: MonoBehaviour
    {
        [SerializeField] private float speed;
        
        private InputSystem _input;
        private Rigidbody2D _rigidbody;
        private float _lastDirection;

        public event Action<float> DirectionChanged;
        
        private void Awake()
        {
            _input = new InputSystem();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            CheckChangedDirection();
        }

        private void CheckChangedDirection()
        {
            var direction = _input.Player.Move.ReadValue<float>();
            
            if (!Mathf.Approximately(direction, _lastDirection))
            {
                ChangeDirection(direction);
            }
            _lastDirection = direction;
        }
        
        private void ChangeDirection(float direction)
        {
            _rigidbody.linearVelocity = new Vector3(direction * speed, _rigidbody.linearVelocity.y);
            DirectionChanged?.Invoke(direction);
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }
    }
}