using R3;
using System;
using UnityEngine;

namespace Testers.R3Samples
{
    public class T6UISwitcher : MonoBehaviour
    {
        [SerializeField] private RectTransform _mainMenuUI;
        [SerializeField] private RectTransform _loadingUI;
        [SerializeField] private RectTransform _gameplayUI;
        [SerializeField] private RectTransform _pauseUI;
        [SerializeField] private RectTransform _gameOverUI;

        private ReactiveProperty<GameState> _gameState = new();

        private enum GameState
        {
            MainMenu,
            Loading,
            Gameplay,
            Pause,
            GameOver
        }

        private void Awake()
        {
            _gameState
                .Select(state => GetPanelFromState(state))
                .DistinctUntilChanged()
                .Subscribe(rectTransform => ShowActivePanel(rectTransform))
                .AddTo(gameObject);

            _gameState.OnNext(GameState.Loading);
            _gameState.OnNext(GameState.MainMenu);
            _gameState.OnNext(GameState.MainMenu);
            _gameState.OnNext(GameState.Gameplay);
            _gameState.OnNext(GameState.Pause);
            _gameState.OnNext(GameState.Pause);
            _gameState.OnNext(GameState.Gameplay);
            _gameState.OnNext(GameState.GameOver);
            _gameState.OnNext(GameState.Loading);
        }

        private void ShowActivePanel(RectTransform rectTransform)
        {
            HideAllPanels();

            rectTransform.gameObject.SetActive(true);
            Debug.Log($"{rectTransform.gameObject.name} activated");
        }

        private void HideAllPanels()
        {
            _mainMenuUI.gameObject.SetActive(false);
            _loadingUI.gameObject.SetActive(false);
            _gameplayUI.gameObject.SetActive(false);
            _pauseUI.gameObject.SetActive(false);
            _gameOverUI.gameObject.SetActive(false);
        }

        private RectTransform GetPanelFromState(GameState state)
        {
            return state switch
            {
                GameState.MainMenu => _mainMenuUI,
                GameState.Loading => _loadingUI,
                GameState.Gameplay => _gameplayUI,
                GameState.Pause => _pauseUI,
                GameState.GameOver => _gameOverUI,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}