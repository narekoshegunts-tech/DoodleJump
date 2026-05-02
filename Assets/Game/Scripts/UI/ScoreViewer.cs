using Game.Scripts.UI.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Core
{
    public class ScoreViewer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        
        [Inject] private ScoreCounter _scoreCounter;
        
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
