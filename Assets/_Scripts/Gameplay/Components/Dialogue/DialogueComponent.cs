using Data.Dialogue;
using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Gameplay.Components
{
    public class DialogueComponent : MonoBehaviour
    {
        [SerializeField] private DialogueSO _dialogue;

        [Inject] private DialogueService _dialogueService;

        public DialogueSO Dialogue => _dialogue;

        public void StartDialogue()
        {
            if (_dialogue == null || _dialogueService == null)
                return;

            _dialogueService.StartDialogue(_dialogue);
        }
    }
}