
using UnityEngine;

namespace Game.Scripts.Feature.Platform
{
    public class Platform: MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.TryGetComponent<Player.Player>(out var player))
            {
                    player.CollidedWithPlatform();
            }
        }
    }
}