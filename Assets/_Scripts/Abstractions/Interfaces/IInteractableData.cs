using UnityEngine.Localization;

namespace Abstractions.Interfaces
{
    public interface IInteractableData
    {
        public LocalizedString InteractionName { get; }
    }
}