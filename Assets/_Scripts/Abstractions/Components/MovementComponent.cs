using System;
using UnityEngine;

namespace Abstractions.Components
{
    public abstract class MovementComponent : MonoBehaviour
    {
        [Min(0)][SerializeField] protected float BaseSpeed;
        [Min(0)][SerializeField] protected float MaxSpeed;

        protected float CurrentSpeed;
        protected float SpeedBonus;

        protected Vector3 CurrentDirection;

        public Action<Vector3> OnMoveStarted;
        public Action OnMoveStopped;
        public Action<Vector3> OnDirectionChanged;
        public Action<float, float> SpeedChanged;

        protected virtual void Awake()
        {
            if(MaxSpeed < BaseSpeed)
                MaxSpeed = BaseSpeed;

            SetSpeed(BaseSpeed);
        }

        public void Move(Vector3 direction)
        {
            direction = direction.normalized;

            if (direction.sqrMagnitude < 0.01f)
            {
                if (CurrentDirection.sqrMagnitude > 0.01f)
                    OnMoveStopped?.Invoke();

                CurrentDirection = Vector3.zero;
                ApplyMovement(Vector3.zero);
                return;
            }

            if (CurrentDirection != direction)
                OnDirectionChanged?.Invoke(direction);

            CurrentDirection = direction;
            OnMoveStarted?.Invoke(direction);

            ApplyMovement(direction * CurrentSpeed);
        }

        protected abstract void ApplyMovement(Vector3 velocity);

        public void SetSpeed(float speed)
        {
            CurrentSpeed = Mathf.Clamp(speed, 0f, MaxSpeed);
            SpeedChanged?.Invoke(CurrentSpeed, MaxSpeed);
        }

        public void AddSpeedBonus(float bonus)
        {
            SpeedBonus += bonus;
            CurrentSpeed = Mathf.Clamp(BaseSpeed + SpeedBonus, 0f, MaxSpeed);
            SpeedChanged?.Invoke(CurrentSpeed, MaxSpeed);
        }

        public void ResetSpeed()
        {
            SpeedBonus = 0f;
            CurrentSpeed = BaseSpeed;
            SpeedChanged?.Invoke(CurrentSpeed, MaxSpeed);
        }

        public void SetBaseSpeed(float newBaseSpeed)
        {
            BaseSpeed = Mathf.Max(0f, newBaseSpeed);
            CurrentSpeed = Mathf.Clamp(BaseSpeed + SpeedBonus, 0f, MaxSpeed);
            SpeedChanged?.Invoke(CurrentSpeed, MaxSpeed);
        }
    }
}