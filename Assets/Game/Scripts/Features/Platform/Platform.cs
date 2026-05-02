
using Game.Scripts.Features.Player;
using UnityEngine;

namespace Game.Scripts.Features.Platform
{
    public class Platform: MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.TryGetComponent<PlayerJumper>(out var playerJumper))
            {
                    playerJumper.CollidedWithPlatform();
            }
        }
    }
}