using UnityEngine;

namespace Game.Scripts.Infrastructure.CameraServices
{
    public class ScreenBoundService
    {
        private Camera _camera = Camera.main;

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