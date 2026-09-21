using System.Collections.Generic;
using Abstractions.Components;
using Abstractions.Components.Interaction;
using UnityEngine;

namespace Gameplay.Components.Interaction
{
    public class Interaction2D : TypedInteractionComponent<InteractionCaster2D, Collider2D>
    {
        [SerializeField] private InteractionCaster2D _caster;

        protected override InteractionCaster2D Caster => _caster;
        protected override Transform Origin => _caster != null ? _caster.Origin : null;

        private void Reset()
        {
            _caster = GetComponent<InteractionCaster2D>();
        }

        protected override IReadOnlyList<Collider2D> GetColliders(LayerMask mask)
        {
            return _caster.GetColliders(mask);
        }
    }
}