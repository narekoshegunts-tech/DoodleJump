using Game.Scripts.Features.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.UI
{
    public class EndGame : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        [SerializeField] private Button _restartButton;
        private PlayerController _playerController;

        [Inject]
        private void Construct(PlayerController playerController)
        {
            _playerController = playerController;
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            _restartButton.interactable = false;
            _canvasGroup.alpha = 0;
        }

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(Restart);
            _playerController.OnDeath += Show;
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(Restart);
            _playerController.OnDeath -= Show;
        }

        private void Show()
        {
            _canvasGroup.alpha = 1;
            _restartButton.interactable = true;
            Time.timeScale = 0;
        }

        private void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
}
