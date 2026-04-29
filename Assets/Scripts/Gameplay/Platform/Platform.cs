using System;
using Gameplay.Player.StateMachine;
using Gameplay.Player.StateMachine.States;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay.Platform
{
    public class Platform: MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.TryGetComponent<PlayerStateMachine>(out var playerStateMachine))
            {
                    playerStateMachine.CollidedWithPlatform();
            }
        }
    }
}