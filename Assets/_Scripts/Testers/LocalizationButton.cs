using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Testers
{
    public class LocalizationButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Locale _locale;

        private void Awake()
        {
            _button.GetComponentInChildren<TMP_Text>().text = _locale.LocaleName;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(SetLocale);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(SetLocale);
        }

        private void SetLocale()
        {
            LocalizationSettings.SelectedLocale = _locale;
        }
    }
}