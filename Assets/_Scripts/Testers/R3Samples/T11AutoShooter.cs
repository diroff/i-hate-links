using R3;
using System;
using UnityEngine;

namespace Testers.R3Samples
{
    public class T11AutoShooter : MonoBehaviour
    {
        [SerializeField] private float _shootingDelay = 0.1f;

        public Observable<bool> IsShooting => _isShooting;
        private ReactiveProperty<bool> _isShooting = new(true);

        private void Awake()
        {
            _isShooting
                .Select(isShooting => isShooting
                    ? Observable.Interval(TimeSpan.FromSeconds(_shootingDelay))
                    : Observable.Empty<Unit>())
                .Switch()
                .Subscribe(_ => Shoot())
                .AddTo(gameObject);
        }

        private void Shoot()
        {
            Debug.Log("Pew");
        }
    }
}