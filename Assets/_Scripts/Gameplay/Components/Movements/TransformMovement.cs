using Abstractions.Components;
using UnityEngine;

namespace Gameplay.Components.Movement
{
    public class TransformMovement : MovementComponent
    {
        private void Update()
        {
            ProcessMovement(Time.deltaTime);
        }

        protected override void ApplyMovement(Vector3 direction, float deltaTime)
        {
            transform.position += direction * CurrentSpeed * deltaTime;
        }
    }
}