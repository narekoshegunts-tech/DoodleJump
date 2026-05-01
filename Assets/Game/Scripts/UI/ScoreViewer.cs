using TMPro;
using UnityEngine;

namespace Game.Scripts.Core
{
    public class ScoreViewer : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;

        private void Update()
        {
            UpdateScore();
        }

        private void UpdateScore()
        {
            int roundedPositionY = (int)Camera.main.transform.position.y;
            scoreText.text = roundedPositionY.ToString();
        }
    }
}
