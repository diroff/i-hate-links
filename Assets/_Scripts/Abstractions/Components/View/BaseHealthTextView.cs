using TMPro;
using UnityEngine;

namespace Abstractions.Components.View
{
    public abstract class BaseHealthTextView<T> : BaseHealthView<T>
    {
        [SerializeField] protected TMP_Text Text;

        protected virtual void Awake()
        {
            if (Text == null)
                Text = GetComponentInChildren<TMP_Text>();
        }

        protected override void UpdateHealthView(T currentValue, T maxValue)
        {
            Text.text = $"{currentValue.ToString()}/{maxValue.ToString()}";
        }
    }
}