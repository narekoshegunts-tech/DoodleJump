using UnityEngine;

namespace Game.Scripts.Features.Common
{
    public class GamePauseService
    {
        public bool IsPaused { get; private set; }

        public void Pause()
        {
            if (IsPaused) return;
            
            Time.timeScale = 0f;
            IsPaused = true;
        }

        public void Resume()
        {
            if (!IsPaused) return;

            Time.timeScale = 1;
            IsPaused = false;
        }
    }
}