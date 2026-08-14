using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Testers.R3Samples
{
    public class T1ButtonSample : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitButton;

        private void Awake()
        {
            var playStream = _playButton.OnClickAsObservable().Select(_ => "Play");
            var settingsStream = _settingsButton.OnClickAsObservable().Select(_ => "Settings");
            var quitStream = _quitButton.OnClickAsObservable().Select(_ => "Quit");

            Observable.Merge(playStream, settingsStream, quitStream)
                .Subscribe(buttonName => Debug.Log($"{buttonName} clicked"))
                .AddTo(gameObject);
        }
    }
}