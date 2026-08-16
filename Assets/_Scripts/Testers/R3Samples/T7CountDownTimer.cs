using R3;
using System;
using UnityEngine;

namespace Testers.R3Samples
{
    public sealed class T7CountDownTimer : MonoBehaviour
    {
        [SerializeField] private int _initialValue = 120;

        public Observable<Unit> Finished => _finished;
        private readonly Subject<Unit> _finished = new();

        public Observable<int> Timer => _timer;
        private ReactiveProperty<int> _timer;
        private readonly Subject<Unit> _skip = new();

        private void Awake()
        {
            _timer = new ReactiveProperty<int>(_initialValue);

            Observable.Interval(TimeSpan.FromSeconds(1))
                .TakeWhile(_ => _timer.Value > 0)
                .TakeUntil(_skip)
                .Subscribe(_ => _timer.Value--, _ => _finished.OnNext(Unit.Default))
                .AddTo(gameObject);
        }

        private void OnDestroy()
        {
            _skip?.Dispose();
        }

        public void Skip()
        {
            _skip.OnNext(Unit.Default);
        }
    }
}