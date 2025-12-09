using Abstractions.Components;
using UnityEngine;

namespace Gameplay.Components.Movement
{
    public class RigidbodyMovement : MovementComponent
    {
        protected Rigidbody Rigidbody;

        protected override void Awake()
        {
            base.Awake();

            Rigidbody = GetComponent<Rigidbody>();
        }

        protected override void ApplyMovement(Vector3 velocity)
        {
            Rigidbody.linearVelocity = velocity;
        }
    }
}