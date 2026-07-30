using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace Testers
{
    public class TestLocalization : MonoBehaviour
    {
        [SerializeField] private LocalizedString _greetingString;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private string _name;

        private void OnEnable()
        {
            _greetingString.StringChanged += OnStringChanged;
        }

        private void OnDisable()
        {
            _greetingString.StringChanged -= OnStringChanged;
        }

        private void Start()
        {
            _greetingString.Arguments = new object[]
            {
                new { name = _name}
            };
        }

        private void OnStringChanged(string value)
        {
            _text.text = value;
        }
    }
}