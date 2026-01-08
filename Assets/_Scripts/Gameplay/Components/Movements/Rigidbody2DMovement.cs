using Abstractions.Components;
using UnityEngine;

namespace Gameplay.Components.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Rigidbody2DMovement : MovementComponent
    {
        protected Rigidbody2D Rigidbody;

        protected override void Awake()
        {
            base.Awake();

            Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            ProcessMovement(Time.fixedDeltaTime);
        }

        public override void DisableMoving()
        {
            base.DisableMoving();

            Rigidbody.linearVelocity = Vector2.zero;
        }

        protected override void ApplyMovement(Vector3 direction, float deltaTime)
        {
            var currentVelocityX = Rigidbody.linearVelocity.x;

            float increment = direction.x * RunAcceleration * deltaTime;

            if (currentVelocityX != 0f && Mathf.Sign(currentVelocityX) != Mathf.Sign(direction.x))
                currentVelocityX = 0f;

            var newSpeed = Mathf.Clamp(currentVelocityX + increment, -CurrentSpeed, CurrentSpeed);
            Rigidbody.linearVelocity = new Vector2(newSpeed, Rigidbody.linearVelocity.y);
        }

        protected override void ApplyHorizontalFriction(float deltaTime)
        {
            base.ApplyHorizontalFriction(deltaTime);

            float vx = Rigidbody.linearVelocity.x * GroundDecay;

            if (Mathf.Abs(vx) < 0.01f)
                vx = 0f;

            Rigidbody.linearVelocity = new Vector2(vx, Rigidbody.linearVelocity.y);
        }

        protected override bool HasMovementInput(Vector3 direction)
        {
            return Mathf.Abs(direction.x) > 0.001f;
        }
    }
}