using Abstractions.Components;
using Abstractions.Interfaces;
using UnityEngine;

namespace Gameplay.Mechanics.Player
{
    public class PlayerView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _visualModel;

        [Header("Observed Components")]
        [SerializeField] private MovementComponent _movement;
        [SerializeField] private JumpComponent _jump;
        [SerializeField] private InteractionComponent _interaction;

        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int IsJumpHash = Animator.StringToHash("IsJump");
        private static readonly int InteractHash = Animator.StringToHash("Interact");

        private void OnEnable()
        {
            if (_movement)
            {
                _movement.OnMoveStarted += OnMoveStarted;
                _movement.OnMoveStopped += OnMoveStopped;
                _movement.OnDirectionChanged += OnDirectionChanged;
            }

            if (_jump)
            {
                _jump.JumpStarted += OnJumpStarted;
                _jump.Landed += OnLanded;
            }

             if (_interaction) 
                _interaction.OnInteracted += PlayInteractAnimation;
        }

        private void OnDisable()
        {
            if (_movement)
            {
                _movement.OnMoveStarted -= OnMoveStarted;
                _movement.OnMoveStopped -= OnMoveStopped;
                _movement.OnDirectionChanged -= OnDirectionChanged;
            }

            if (_jump)
            {
                _jump.JumpStarted -= OnJumpStarted;
                _jump.Landed -= OnLanded;
            }
        }

        private void OnMoveStarted(Vector3 direction) => _animator.SetFloat(SpeedHash, 1f);
        private void OnMoveStopped() => _animator.SetFloat(SpeedHash, 0f);

        private void OnJumpStarted() => _animator.SetBool(IsJumpHash, true);
        private void OnLanded() => _animator.SetBool(IsJumpHash, false);

        private void PlayInteractAnimation(IInteractable interactable) => _animator.SetTrigger(InteractHash);

        private void OnDirectionChanged(Vector3 direction)
        {
            if (Mathf.Approximately(direction.x, 0f)) 
                return;

            float yRotation = direction.x > 0 ? 0f : 180f;
            _visualModel.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}