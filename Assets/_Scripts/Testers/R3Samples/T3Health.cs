using R3;
using UnityEngine;

namespace Testers.R3Samples
{
    public class T3Health : MonoBehaviour
    {
        private readonly ReactiveProperty<int> _health = new ReactiveProperty<int>(100);

        private void Awake()
        {
            _health.AsObservable()
                .Subscribe(value => Debug.Log($"Health: {value}"))
                .AddTo(gameObject);

/*            _health.AsObservable()
                .Where(hp => hp <= 0)
                .Take(1)
                .Subscribe(value => Debug.Log("Is dead"))
                .AddTo(gameObject);*/

            _health.AsObservable()
                .Select(health => health <= 0)
                .DistinctUntilChanged()
                .Where(isDead => isDead)
                .Subscribe(_ => Debug.Log("Is dead"))
                .AddTo(gameObject);

            _health.Value = 80;
            _health.Value = 80;
            _health.Value = 60;
            _health.Value = 25;
            _health.Value = 10;
            _health.Value = 0;
            _health.Value = 0;
            _health.Value = 0;
            _health.Value = -10;
            _health.Value = 10;
            _health.Value = 20;
            _health.Value = 0;
        }
    }
}