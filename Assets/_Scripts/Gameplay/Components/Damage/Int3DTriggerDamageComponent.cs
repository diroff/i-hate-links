using Abstractions.Components.Damage;
using UnityEngine;

namespace Gameplay.Components.Damage
{
    public class Int3DTriggerDamageComponent : ZoneIntDamageComponent
    {
        private void OnTriggerEnter(Collider other) => HandleEnter(other.gameObject);
        private void OnTriggerExit(Collider other) => HandleExit(other.gameObject);
    }
}