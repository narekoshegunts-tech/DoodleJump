using UnityEngine;

namespace Game.Scripts.Features.Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerVisual: MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _playerController.OnDirectionChanged += FlipVisual;
        }

        private void OnDisable()
        {
            _playerController.OnDirectionChanged -= FlipVisual;
        }

        private void FlipVisual(float direction)
        {
            _spriteRenderer.flipX = direction < 0;
        }
        
    }
}