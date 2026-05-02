using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure.CameraServices
{
    public class ScreenBoundService
    {
        [Inject] private Camera _camera;

        public float GetScreenBoundMinPositionX()
        {
            return _camera.ViewportToWorldPoint(Vector3.zero).x;
        }

        public float GetScreenBoundMaxPositionX()
        {
            return _camera.ViewportToWorldPoint(Vector3.one).x;
        }

        public float GetScreenBoundMinPositionY()
        {
            return _camera.ViewportToWorldPoint(Vector3.zero).y;
        }
    }
}