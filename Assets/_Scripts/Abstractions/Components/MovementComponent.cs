using System;
using UnityEngine;

namespace Abstractions.Components
{
    public abstract class MovementComponent : MonoBehaviour
    {
        [Min(0)][SerializeField] protected float BaseSpeed;
        [Min(0)][SerializeField] protected float MaxSpeed;
        [Min(0)][SerializeField] protected float RunAcceleration;
        [Range(0f, 1f)][SerializeField] protected float GroundDecay;

        protected float CurrentSpeed;
        protected float SpeedBonus;

        protected Vector3 DesiredDirection;
        protected Vector3 CurrentDirection;
        protected bool IsMoving;

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

        public void Move(Vector3 inputDirection)
        {
            inputDirection = inputDirection.normalized;
            DesiredDirection = inputDirection;

            bool hasInput = HasMovementInput(inputDirection);

            if (HandleStartMove(hasInput, inputDirection)) 
                return;

            if (HandleStopMove(hasInput)) 
                return;

            HandleDirectionChange(hasInput, inputDirection);
        }

        private bool HandleStartMove(bool hasInput, Vector3 inputDirection)
        {
            if (!IsMoving && hasInput)
            {
                IsMoving = true;
                CurrentDirection = inputDirection;
                OnMoveStarted?.Invoke(inputDirection);
                return true;
            }

            return false;
        }

        private bool HandleStopMove(bool hasInput)
        {
            if (IsMoving && !hasInput)
            {
                IsMoving = false;
                OnMoveStopped?.Invoke();
                return true;
            }

            return false;
        }

        private void HandleDirectionChange(bool hasInput, Vector3 inputDirection)
        {
            if (CurrentDirection != inputDirection)
            {
                CurrentDirection = inputDirection;
                OnDirectionChanged?.Invoke(inputDirection);
            }
        }

        protected void ProcessMovement(float deltaTime)
        {
            if (IsMoving)
                ApplyMovement(DesiredDirection, deltaTime);
            else
                ApplyHorizontalFriction(deltaTime);
        }

        protected abstract void ApplyMovement(Vector3 direction, float deltaTime);
        protected virtual void ApplyHorizontalFriction(float deltaTime) { }

        protected virtual bool HasMovementInput(Vector3 direction)
        {
            return direction.sqrMagnitude > 0.001f;
        }

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