using System.Collections.Generic;
using Abstractions.Components;
using Abstractions.Components.Interaction;
using UnityEngine;

namespace Gameplay.Components.Interaction
{
    public class Interaction3D : TypedInteractionComponent<InteractionCaster3D, Collider>
    {
        [SerializeField] private InteractionCaster3D _caster;

        protected override InteractionCaster3D Caster => _caster;
        protected override Transform Origin => _caster != null ? _caster.Origin : null;

        private void Reset()
        {
            _caster = GetComponent<InteractionCaster3D>();
        }

        protected override IReadOnlyList<Collider> GetColliders(LayerMask mask)
        {
            return _caster.GetColliders(mask);
        }
    }
}