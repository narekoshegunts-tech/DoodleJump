using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Features.Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerVisual: MonoBehaviour
    {
        [SerializeField] private PlayerMover _playerMover;
        
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _playerMover.OnDirectionChanged += FlipVisual;
        }

        private void OnDisable()
        {
            _playerMover.OnDirectionChanged -= FlipVisual;
        }

        private void FlipVisual(float direction)
        {
            _spriteRenderer.flipX = direction < 0;
        }
        
    }
}