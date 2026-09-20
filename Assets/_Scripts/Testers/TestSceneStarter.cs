using Gameplay.Components;
using UnityEngine;

namespace Testers
{
    public class TestSceneStarter : MonoBehaviour
    {
        [SerializeField] private DialogueComponent _firstDialogue;

        private void Start()
        {
            _firstDialogue.StartDialogue();
        }
    }
}