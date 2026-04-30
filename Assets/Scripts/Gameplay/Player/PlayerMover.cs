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

        private float _maxPositionX;
        private float _minPositionX;

        public event Action<float> DirectionChanged;
        
        private void Awake()
        {
            _input = new InputSystem();
            _rigidbody = GetComponent<Rigidbody2D>();
            
            SetMinMaxPositionX();
        }

        private void SetMinMaxPositionX()
        {
            var camera = UnityEngine.Camera.main;
            _minPositionX = camera.ViewportToWorldPoint(Vector3.zero).x;
            _maxPositionX = camera.ViewportToWorldPoint(Vector3.one).x;
        }

        private void Update()
        {
            CheckChangedDirection();
            Move();
        }

        private void CheckChangedDirection()
        {
            var direction = _input.Player.Move.ReadValue<Vector2>().x;
            
            if ((direction > 0 && _lastDirection < 0) || 
                (direction < 0 && _lastDirection > 0))
            {
                ChangeDirection(direction);
            }
            
            _lastDirection = direction;
        }

        private void Move()
        {
            transform.Translate(Vector2.right * (speed * _lastDirection * Time.deltaTime));
            if (transform.position.x > _maxPositionX)
            {
                transform.position = new Vector2(_maxPositionX, transform.position.y);
            }
            else if (transform.position.x < _minPositionX)
            {
                transform.position = new Vector2(_minPositionX, transform.position.y);
            }
        }

        private void ChangeDirection(float direction)
        {
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