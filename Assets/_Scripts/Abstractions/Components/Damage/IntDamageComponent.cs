using UnityEngine;

namespace Abstractions.Components.Damage
{
    public class IntDamageComponent : BaseDamageComponent<int>
    {
        [SerializeField] protected int BaseDamage;

        public override int CalculateCurrentDamageValue()
        {
            return BaseDamage;
        }
    }
}