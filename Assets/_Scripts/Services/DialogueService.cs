using Data.Dialogue;
using System;

namespace Services
{
    public class DialogueService
    {
        public event Action<DialogueNode> OnNodeStarted;
        public event Action OnDialogueEnded;

        public bool IsActive { get; private set; }
        public DialogueNode CurrentNode => _currentDialogue.Nodes[_currentIndex];

        private DialogueSO _currentDialogue;
        private int _currentIndex;

        public void StartDialogue(DialogueSO dialogue)
        {
            if (dialogue == null || dialogue.Nodes.Count == 0)
                return;

            _currentDialogue = dialogue;
            _currentIndex = 0;
            IsActive = true;

            ShowCurrentNode();
        }

        public void Advance()
        {
            if (!IsActive)
                return;

            _currentIndex++;

            if (_currentIndex < _currentDialogue.Nodes.Count)
                ShowCurrentNode();
            else
                EndDialogue();
        }

        public void EndDialogue()
        {
            if (!IsActive)
                return;

            IsActive = false;
            _currentDialogue = null;
            OnDialogueEnded?.Invoke();
        }

        private void ShowCurrentNode()
        {
            OnNodeStarted?.Invoke(CurrentNode);
        }
    }
}