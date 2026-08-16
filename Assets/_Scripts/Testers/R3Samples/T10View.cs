using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Testers.R3Samples
{
    public class T10View : MonoBehaviour
    {
        [SerializeField] private T10Ability _ability;
        [SerializeField] private Button _castButton;
        [SerializeField] private TMP_Text _manaCountText;
        [SerializeField] private TMP_Text _aliveStatusText;

        private void Start()
        {
            _ability.CanCast
                .Subscribe(canCast => _castButton.interactable = canCast)
                .AddTo(gameObject);

            _ability.Mana
                .DistinctUntilChanged()
                .Subscribe(value => _manaCountText.text = value.ToString())
                .AddTo(gameObject);

            _ability.IsAlive
                .DistinctUntilChanged()
                .Subscribe(isAlive => _aliveStatusText.text = "Is alive: " + isAlive)
                .AddTo(gameObject);

            _castButton.OnClickAsObservable()
                .Subscribe(_ => _ability.Cast())
                .AddTo(gameObject);
        }
    }
}