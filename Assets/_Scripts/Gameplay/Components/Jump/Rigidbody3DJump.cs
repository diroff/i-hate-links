using Abstractions.Components;
using UnityEngine;

namespace Gameplay.Components.Jump
{
    [RequireComponent(typeof(Rigidbody))]
    public class Rigidbody3DJump : JumpComponent
    {
        protected override void PerformJump()
        {
            throw new System.NotImplementedException();
        }
    }
}