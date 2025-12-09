using Reflex.Extensions;
using Reflex.Injectors;
using UnityEngine;

namespace Testers
{
    public class TestKey : MonoBehaviour, IInteractable
    {
        private SpriteRenderer _spriteRenderer;

        public bool IsPickedUp { get; private set; }
        public bool CanInteract(GameObject interactor) => !IsPickedUp;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Interact(GameObject interactor)
        {
            if (IsPickedUp)
                return;

            IsPickedUp = true;
            _spriteRenderer.enabled = false;
            GetComponent<Collider2D>().enabled = false;

            Debug.Log("Key is collected");
        }
    }
}