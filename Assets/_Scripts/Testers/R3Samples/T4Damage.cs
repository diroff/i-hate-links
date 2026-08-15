using R3;
using UnityEngine;

namespace Testers.R3Samples
{
    public class T4Damage : MonoBehaviour
    {
        private Subject<int> _damage = new();

        private void Awake()
        {
            _damage
                .Where(value => value >= 20)
                .Select(value => $"Received {value} damage")
                .Subscribe(message => Debug.Log(message))
                .AddTo(gameObject);

            _damage.OnNext(5);
            _damage.OnNext(20);
            _damage.OnNext(3);
            _damage.OnNext(100);
            _damage.OnNext(7);
            _damage.OnNext(50);
        }
    }
}