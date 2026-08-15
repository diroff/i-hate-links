using R3;
using UnityEngine;

namespace Testers.R3Samples
{
    public class T5Death : MonoBehaviour
    {
        private readonly Subject<int> _damage = new();
    
        private void Awake()
        {
            _damage
                .Scan(100, (health, damage) => health - damage)
                .Subscribe(health => Debug.Log($"Health: {health}"))
                .AddTo(gameObject);

            _damage.OnNext(10);
            _damage.OnNext(20);
            _damage.OnNext(30);
            _damage.OnNext(50);
        }
    }
}