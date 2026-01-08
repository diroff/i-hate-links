using UnityEngine;

namespace Abstractions.Components.View
{
    public abstract class BaseHealthView<T> : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            SubscribeToHealthEvents();
        }

        protected virtual void OnDisable()
        {
            UnsubscribeToHealthEvents();
        }

        protected virtual void Start()
        {
            InitializeHealthView();
        }

        protected abstract void InitializeHealthView();
        protected abstract void SubscribeToHealthEvents();
        protected abstract void UnsubscribeToHealthEvents();
        protected abstract void UpdateHealthView(T currentValue, T maxValue);
    }
}