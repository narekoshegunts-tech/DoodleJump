using Game.Scripts.Features.Player;
using Game.Scripts.UI.Services.Signals;
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
        
        [Inject] private SignalBus _signalBus;
        

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
            _restartButton.onClick.AddListener(OnRestartClick);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartClick);
        }

        public void Show()
        {
            _canvasGroup.alpha = 1;
            _restartButton.interactable = true;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0f;
            _restartButton.interactable = false;
        }

        private void OnRestartClick()
        {
            _signalBus.Fire<RestartRequestSignal>();
            Hide();
        }
    }
}
