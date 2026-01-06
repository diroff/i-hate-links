using Abstractions.Components;
using UnityEngine;

namespace Gameplay.Components.Jump
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Rigidbody2DJump : JumpComponent
    {
        private Rigidbody2D _rigidbody;

        protected override void Awake()
        {
            base.Awake();
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.gravityScale = GravityScale;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            ApplyGravity();
            ClampFallSpeed();
        }

        protected override void PerformJump()
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, JumpForce);
            InvokeJumpStarted();
        }

        private void ApplyGravity()
        {
            float gravityScale = GravityScale;

            if (_rigidbody.linearVelocity.y < 0f)
            {
                // falling
                gravityScale *= FallGravityMultiplier;
            }
            else if (Mathf.Abs(_rigidbody.linearVelocity.y) < JumpHangTimeThreshold)
            {
                // jump hang (near apex)
                gravityScale *= JumpHangGravityMultiplier;
            }
            else if (!HandleLongJumps)
            {
                // jump cut (button released)
                gravityScale *= JumpCutGravity;
            }

            _rigidbody.gravityScale = gravityScale;
        }

        private void ClampFallSpeed()
        {
            if (_rigidbody.linearVelocity.y < -MaxFallSpeed)
                _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, -MaxFallSpeed);
        }
    }
}