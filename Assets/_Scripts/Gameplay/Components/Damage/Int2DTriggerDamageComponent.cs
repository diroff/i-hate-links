using Abstractions.Components.Damage;
using UnityEngine;

namespace Gameplay.Components.Damage
{
    public class Int2DTriggerDamageComponent : ZoneIntDamageComponent
    {
        private void OnTriggerEnter2D(Collider2D other) => HandleEnter(other.gameObject);
        private void OnTriggerExit2D(Collider2D other) => HandleExit(other.gameObject);
    }
}