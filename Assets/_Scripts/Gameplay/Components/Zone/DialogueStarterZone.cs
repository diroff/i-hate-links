using Abstractions.Components.Zone;
using Gameplay.Mechanics.Player;
using UnityEngine;

namespace Gameplay.Components.Zone
{
    public class DialogueStarterZone : MonoBehaviour
    {
        [SerializeField] private Zone2DBase _zone;
        [SerializeField] private DialogueComponent _dialogue;

        private void Reset()
        {
            _zone = GetComponent<Zone2DBase>();
            _dialogue = GetComponent<DialogueComponent>();
        }

        private void OnEnable()
        {
            _zone.OnEnter += OnEnter;
        }

        private void OnDisable()
        {
            _zone.OnEnter -= OnEnter;
        }

        private void OnEnter(Collider2D collider)
        {
            if (!collider.gameObject.GetComponent<Player>())
                return;

            _dialogue.StartDialogue();
        }
    }
}