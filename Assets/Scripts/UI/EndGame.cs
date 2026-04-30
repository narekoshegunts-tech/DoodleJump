using System;
using Gameplay.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class EndGame : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    [SerializeField] private Button _restartButton;
    private Player _player;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
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
        _player.OnDie += Show;
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(Restart);
        _player.OnDie -= Show;
    }

    private void Show()
    {
        _canvasGroup.alpha = 1;
        _restartButton.interactable = true;
        Time.timeScale = 0;
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
