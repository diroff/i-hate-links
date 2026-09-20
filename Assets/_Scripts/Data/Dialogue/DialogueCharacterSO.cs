using UnityEngine;
using UnityEngine.Localization;

namespace Data.Dialogue
{
    [CreateAssetMenu(menuName = "Data/Dialogue/Character")]
    public class DialogueCharacterSO : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public LocalizedString Name { get; private set; }
        [field: SerializeField] public Sprite Portrait { get; private set; }
    }
}