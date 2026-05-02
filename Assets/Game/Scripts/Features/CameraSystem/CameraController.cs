using Game.Scripts.Features.Player;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.CameraSystem
{
    public class CameraController: MonoBehaviour
    {
        private Transform _playerTransform;
        
        [Inject]
        private void Construct(PlayerDeathDetector player)
        {
            _playerTransform = player.transform;
        }

        private void LateUpdate()
        {
            MoveCamera();
        }

        private void MoveCamera()
        {
            if (_playerTransform.position.y > transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, 
                                                _playerTransform.position.y, 
                                                transform.position.z);
            }
        }
    }
}
