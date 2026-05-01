using UnityEngine;

namespace Game.Scripts.Features.Player
{
    public class PlayerVisual: MonoBehaviour
    {
        [SerializeField] private PlayerMover _playerMover;
        
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _playerMover.DirectionChanged += FlipVisual;
        }

        private void FlipVisual(float direction)
        {
            _spriteRenderer.flipX = direction < 0;
        }
        
    }
}