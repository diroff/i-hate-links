using System;
using UnityEngine;
using UnityEngine.Localization;

namespace Data.Dialogue
{
    [Serializable]
    public struct DialogueNode
    {
        [SerializeField] private DialogueCharacterSO _speaker;
        [SerializeField] private LocalizedString _text;

        public DialogueCharacterSO Speaker => _speaker;
        public LocalizedString Text => _text;
    }
}