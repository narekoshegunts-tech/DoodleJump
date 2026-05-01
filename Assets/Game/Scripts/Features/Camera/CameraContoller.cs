using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Camera
{
    public class CameraContoller: MonoBehaviour
    {
        private Transform _playerTransform;
        
        [Inject]
        private void Construct(Player.Player player)
        {
            _playerTransform = player.transform;
        }

        private void Update()
        {
            MoveCamera();
        }

        private void MoveCamera()
        {
            if (_playerTransform.position.y > gameObject.transform.position.y)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 
                                                            _playerTransform.position.y, 
                                                            gameObject.transform.position.z);
            }
        }
    }
}