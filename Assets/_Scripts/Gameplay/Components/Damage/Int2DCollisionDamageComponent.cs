using Abstractions.Components.Damage;
using UnityEngine;

namespace Gameplay.Components.Damage
{
    public class Int2DCollisionDamageComponent : ZoneIntDamageComponent
    {
        private void OnCollisionEnter2D(Collision2D collision) => HandleEnter(collision.gameObject);

        private void OnCollisionExit2D(Collision2D collision) => HandleExit(collision.gameObject);
    }
}