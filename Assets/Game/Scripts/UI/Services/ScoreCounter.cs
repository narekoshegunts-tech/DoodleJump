using UnityEngine;

namespace Game.Scripts.UI.Services
{
    public class ScoreCounter
    {
        private Camera _camera;

        public ScoreCounter()
        {
            _camera = Camera.main;
        }

        public int GetScore()
        {
            return Mathf.FloorToInt(_camera.transform.position.y);
        }
    }
}