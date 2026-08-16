using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Testers.R3Samples
{
    public sealed class T7MatchTimerView : MonoBehaviour
    {
        [SerializeField] private T7CountDownTimer _timer;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _skipButton;
        [SerializeField] private float _finishDelay;
        [SerializeField] private RectTransform _finishPanel;

        private void Start()
        {
            _timer.Timer
                .Subscribe(UpdateView, _ => Debug.Log("View: timer finished"))
                .AddTo(gameObject);

            _timer.Finished
                .Do(_ => _skipButton.gameObject.SetActive(false))
                .Delay(TimeSpan.FromSeconds(_finishDelay))
                .Subscribe(_ => ShowFinishedPanel())
                .AddTo(gameObject);

            _skipButton.OnClickAsObservable()
                .Subscribe(_ => _timer.Skip())
                .AddTo(gameObject);
        }

        private void UpdateView(int seconds)
        {
            _text.text = seconds.ToString();
        }

        private void ShowFinishedPanel()
        {
            _finishPanel.gameObject.SetActive(true);
        }
    }
}