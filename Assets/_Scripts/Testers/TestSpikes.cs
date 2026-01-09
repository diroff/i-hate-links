using Abstractions.Components.Damage;
using Abstractions.Interfaces;
using DG.Tweening;
using Gameplay.Components.Health;
using UnityEngine;

namespace Testers
{
    public class TestSpikes : MonoBehaviour
    {
        [SerializeField] private GameObject _view;

        private IntDamageComponent _damage;
        private IntHealthComponent _health;

        private void Awake()
        {
            _damage = GetComponent<IntDamageComponent>();
            _health = GetComponent<IntHealthComponent>();
        }

        private void OnEnable()
        {
            _damage.OnDealDamage += OnDealDamage;
            _health.OnDied += OnDied;
        }

        private void OnDisable()
        {
            _damage.OnDealDamage -= OnDealDamage;
            _health.OnDied -= OnDied;
        }

        private void OnDestroy()
        {
            _view?.transform.DOKill();
        }

        private void OnDealDamage(IDamageable<int> damageable, int damageValue)
        {
            if (_view == null)
                return;

            _damage.DisableDamage();
            _health.Damage(damageValue, gameObject);

            _view.transform.DOKill();
            _view.transform.localScale = Vector3.one;
            _view.transform.DOScale(1.25f, 0.25f)
                .SetEase(Ease.OutBounce)
                .OnComplete(OnDealDamageAnimationCompleted);
        }

        private void OnDealDamageAnimationCompleted()
        {
            if (_health.IsDead)
                return;

            _damage?.EnableDamage();
        }

        private void OnDied(GameObject dead, GameObject killer)
        {
            _damage.DisableDamage();

            transform.DOScale(Vector3.zero, 0.5f)
                .SetEase(Ease.OutBounce)
                .OnComplete(() => Destroy(gameObject));
        }
    }
}