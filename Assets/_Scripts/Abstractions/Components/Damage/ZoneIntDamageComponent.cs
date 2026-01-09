using Abstractions.Interfaces;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Abstractions.Components.Damage
{
    public class ZoneIntDamageComponent : IntDamageComponent
    {
        [Header("Base settings")]
        [SerializeField] protected LayerMask DamageMask = ~0;
        [SerializeField] protected float AttackPeriod = 0f;

        protected readonly HashSet<IDamageable<int>> ActiveTargets = new();
        protected readonly Dictionary<IDamageable<int>, CancellationTokenSource> AttackTokens = new();

        protected override bool CanDamage(GameObject obj)
        {
            return base.CanDamage(obj) && ((1 << obj.layer) & DamageMask) != 0;
        }

        protected void HandleEnter(GameObject other)
        {
            if (!CanDamage(other))
                return;

            if (!other.TryGetComponent(out IDamageable<int> damageable))
                return;

            DealDamage(damageable);

            if (AttackPeriod > 0)
                StartPeriodicDamage(damageable);
        }

        protected void HandleExit(GameObject other)
        {
            if (other.TryGetComponent(out IDamageable<int> damageable))
                StopPeriodicDamage(damageable);
        }

        protected async UniTaskVoid PeriodicDamageRoutine(IDamageable<int> target, CancellationToken ct)
        {
            if (AttackPeriod <= 0)
                return;

            while (!ct.IsCancellationRequested && ActiveTargets.Contains(target))
            {
                await UniTask.Delay((int)(AttackPeriod * 1000), cancellationToken: ct);
                DealDamage(target);
            }
        }

        protected void StartPeriodicDamage(IDamageable<int> target)
        {
            if (AttackPeriod <= 0 || AttackTokens.ContainsKey(target))
                return;

            ActiveTargets.Add(target);

            var cts = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy());
            AttackTokens[target] = cts;

            PeriodicDamageRoutine(target, cts.Token).Forget();
        }

        protected void StopPeriodicDamage(IDamageable<int> target)
        {
            if (!ActiveTargets.Remove(target))
                return;

            if (!AttackTokens.TryGetValue(target, out var cts))
                return;

            cts?.Cancel();
            cts?.Dispose();
            AttackTokens.Remove(target);
        }
    }
}