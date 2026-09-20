using System.Collections.Generic;
using UnityEngine;

namespace Data.Dialogue
{
    [CreateAssetMenu(menuName = "Data/Dialogue/Sequence")]
    public class DialogueSO : ScriptableObject
    {
        [SerializeField] private List<DialogueNode> _nodes = new();

        public IReadOnlyList<DialogueNode> Nodes => _nodes;
    }
}