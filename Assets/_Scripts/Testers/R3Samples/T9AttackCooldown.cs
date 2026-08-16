using R3;
using R3.Triggers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Testers.R3Samples
{
    public class T9AttackCooldown : MonoBehaviour
    {
        [SerializeField] private Button _attackButton;
        [SerializeField] private float _attackCooldown;

        private void Awake()
        {
            _attackButton.OnPointerClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(_attackCooldown))
                .Subscribe(_ => Debug.Log("Attack"))
                .AddTo(gameObject);
        }
    }
}