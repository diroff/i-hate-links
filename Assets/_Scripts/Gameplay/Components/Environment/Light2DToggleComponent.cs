using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Gameplay.Components.Environment
{
    public class Light2DToggleComponent : MonoBehaviour
    {
        [SerializeField] private Light2D _light;

        private void Reset()
        {
            if(_light == null)
                _light = GetComponent<Light2D>();
        }

        public void Toggle()
        {
            if (_light == null)
                return;

            _light.enabled = !_light.enabled;
        }
    }
}