using R3;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Testers.R3Samples
{
    public class T13Clicks : MonoBehaviour
    {
        [SerializeField] private float _doubleClickTime = 0.25f;

        private readonly Subject<Unit> _clicksInput = new Subject<Unit>();

        private void Awake()
        {
            var clickWindowEnder = _clicksInput.Debounce(TimeSpan.FromSeconds(_doubleClickTime));

            _clicksInput
                .Chunk(clickWindowEnder)
                .Subscribe(clicksArray =>
                {
                    if (clicksArray.Length == 1)
                        ExecuteSingleClick();
                    else if (clicksArray.Length >= 2)
                        ExecuteDoubleClick();
                })
                .AddTo(gameObject);
        }

        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
                _clicksInput.OnNext(Unit.Default);
        }

        private void ExecuteSingleClick()
        {
            Debug.Log($"Single click");
        }

        private void ExecuteDoubleClick()
        {
            Debug.Log($"Double click");
        }
    }
}