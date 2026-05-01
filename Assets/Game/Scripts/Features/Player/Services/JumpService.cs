using UnityEngine;

namespace Game.Scripts.Features.Player.Services
{
    public class JumpService
    {
        private Rigidbody2D _rigidbody2D;
        private float _jumpForce;

        public void SetProperties(Rigidbody2D rigidbody2D, float jumpForce)
        {
            _rigidbody2D = rigidbody2D;
            _jumpForce = jumpForce;
        }
        
        private void Jump()
        {
            if (_rigidbody2D.velocity.y > 0)
                return; 
            
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        public void CollidedWithPlatform()
        {
            Jump();
        }
    }
}