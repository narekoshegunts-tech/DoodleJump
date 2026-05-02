using UnityEngine;
using Zenject;

namespace Game.Scripts.UI.Services
{
    public class ScoreCounter
    {
        [Inject] private Camera _camera;

        public int GetScore()
        {
            return Mathf.FloorToInt(_camera.gameObject.transform.position.y);
        }
    }
}