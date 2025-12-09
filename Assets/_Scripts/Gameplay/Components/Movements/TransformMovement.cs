using Abstractions.Components;
using UnityEngine;

namespace Gameplay.Components.Movement
{
    public class TransformMovement : MovementComponent
    {
        protected override void ApplyMovement(Vector3 velocity)
        {
            transform.position += velocity * Time.deltaTime;
        }
    }
}