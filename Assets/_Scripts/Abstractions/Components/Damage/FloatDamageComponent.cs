using UnityEngine;

namespace Abstractions.Components.Damage
{
    public class FloatDamageComponent : BaseDamageComponent<float>
    {
        [SerializeField] protected float BaseDamage;

        public override float CalculateCurrentDamageValue()
        {
            return BaseDamage;
        }
    }
}