using R3;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Testers.R3Samples
{
    public class T10Ability : MonoBehaviour
    {
        [SerializeField] private int _abilityCost = 20;
        [SerializeField] private float _baseCooldown = 1f;

        public Observable<int> Mana => _mana;
        public Observable<float> Cooldown => _cooldown;
        public Observable<bool> IsAlive => _isAlive;
        public Observable<bool> CanCast => _canCast;

        private readonly ReactiveProperty<int> _mana = new(100);
        private readonly ReactiveProperty<float> _cooldown = new(0);
        private readonly ReactiveProperty<bool> _isAlive = new(true);
        private readonly ReactiveProperty<bool> _canCast = new();

        private void Awake()
        {
            Observable
                .CombineLatest(_mana,_cooldown, _isAlive, 
                (mana, cooldown, isAlive) => mana >= _abilityCost && cooldown <= 0 && isAlive)
                .Subscribe(value => _canCast.Value = value)
                .AddTo(gameObject);
        }

        public void Cast()
        {
            if (!_canCast.Value)
                return;

            _mana.Value -= _abilityCost;
            _cooldown.Value = _baseCooldown;

            Observable.Interval(TimeSpan.FromSeconds(0.1f))
                .TakeWhile(_ => _cooldown.Value > 0)
                .Subscribe(_ => _cooldown.Value = Mathf.Max(0, _cooldown.Value - 0.1f))
                .AddTo(gameObject);
        }

        private void Update()
        {
            if (Keyboard.current.aKey.wasPressedThisFrame)
                AddMana(10);

            if (Keyboard.current.dKey.wasPressedThisFrame)
                SetAlive(false);

            if (Keyboard.current.sKey.wasPressedThisFrame)
                SetAlive(true);
        }

        public void AddMana(int value) => _mana.Value += value;
        public void SetAlive(bool value) => _isAlive.Value = value;
    }
}