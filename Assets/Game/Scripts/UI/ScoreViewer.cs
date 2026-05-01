using Game.Scripts.UI.Services;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Core
{
    public class ScoreViewer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        
        private ScoreCounter _scoreCounter;

        private void Awake()
        {
            _scoreCounter = new ScoreCounter();
        }
        private void Update()
        {
            UpdateScore();
        }

        private void UpdateScore()
        {
            int roundedPositionY = _scoreCounter.GetScore();
            _scoreText.text = roundedPositionY.ToString();
        }
    }
}
