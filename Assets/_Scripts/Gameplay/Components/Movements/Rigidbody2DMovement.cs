using Abstractions.Components;
using UnityEngine;

namespace Gameplay.Components.Movement
{
    public class Rigidbody2DMovement : MovementComponent
    {
        protected Rigidbody2D Rigidbody;

        protected override void Awake()
        {
            base.Awake();

            Rigidbody = GetComponent<Rigidbody2D>();
        }

        protected override void ApplyMovement(Vector3 velocity)
        {
            Rigidbody.linearVelocity = velocity;
        }
    }
}