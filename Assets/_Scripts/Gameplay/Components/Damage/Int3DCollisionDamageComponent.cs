using Abstractions.Components.Damage;
using UnityEngine;

namespace Gameplay.Components.Damage
{
    public class Int3DCollisionDamageComponent : ZoneIntDamageComponent
    {
        private void OnCollisionEnter(Collision collision) => HandleEnter(collision.gameObject);

        private void OnCollisionExit(Collision collision) => HandleExit(collision.gameObject);
    }
}