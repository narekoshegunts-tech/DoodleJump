using Game.Scripts.Features.Player.Services;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerJumper: MonoBehaviour
    {
        private JumpService _jumpService;
        
        private Rigidbody2D _rigidbody2D;
        
        [SerializeField] float _jumpForce;
        
        [Inject]
        private void Construct(JumpService.Factory jumpServiceFactory)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            
            _jumpService = jumpServiceFactory.Create(_rigidbody2D, _jumpForce);
        }
        
        public void CollidedWithPlatform()
        {
            _jumpService.CollidedWithPlatform();
        }
        
    }
}