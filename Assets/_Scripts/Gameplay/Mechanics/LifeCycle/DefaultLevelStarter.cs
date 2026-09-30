using Abstractions.Mechanics.Lifecycle;
using Cysharp.Threading.Tasks;
using Gameplay.Components;
using Reflex.Attributes;
using Services;
using System.Threading;
using UnityEngine;

namespace Gameplay.Mechanics.Lifecycle
{
    public class DefaultLevelStarter : LevelStarter
    {
        [Header("Default Starter Settings")]
        [SerializeField] private DialogueComponent _starterDialogue;

        [Inject] private InputService _inputService;

        protected override void OnBeforeStart()
        {
            base.OnBeforeStart();

            _inputService.EnableGameplay();
        }

        protected override async UniTask OnStartProcessAsync(CancellationToken cancellationToken)
        {
            StartDialogue();

            await UniTask.CompletedTask;
        }

        protected override void OnAfterStart()
        {
            base.OnAfterStart();
        }

        private void StartDialogue()
        {
            if (_starterDialogue == null)
                return;

            _starterDialogue.StartDialogue();
        }
    }
}