using R3;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Testers.R3Samples
{
    public class T12Combo : MonoBehaviour
    {
        private readonly Subject<Unit> _attackInput = new Subject<Unit>();

        private enum ComboSignal { Attack, Timeout }

        private void Start()
        {
            var attacks = _attackInput.Select(_ => ComboSignal.Attack);

            var timeouts = _attackInput
                .Debounce(TimeSpan.FromMilliseconds(700))
                .Select(_ => ComboSignal.Timeout);

            Observable.Merge(attacks, timeouts)
                .Scan(0, (comboCount, signal) =>
                {
                    if (signal == ComboSignal.Timeout)
                    {
                        Debug.Log("Combo Reset!");
                        return 0;
                    }

                    return (comboCount % 3) + 1;
                })
                .Where(comboCount => comboCount > 0)
                .Subscribe(comboStep => ExecuteAttack(comboStep))
                .AddTo(gameObject);
        }

        private void Update()
        {
            if(Mouse.current.leftButton.wasPressedThisFrame)
                _attackInput.OnNext(Unit.Default);
        }

        private void ExecuteAttack(int step)
        {
            Debug.Log($"Hit: {step}");
        }
    }
}