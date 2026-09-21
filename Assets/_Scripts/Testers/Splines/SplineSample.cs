using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

namespace Testers.Splines
{
    public class SplineSample : MonoBehaviour
    {
        [SerializeField] private SplineContainer _splineContainer;
        [SerializeField] private Slider _slider;

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void Start()
        {
            OnValueChanged(_slider.value);
        }

        private void OnValueChanged(float value)
        {
            var position = _splineContainer.EvaluatePosition(value);
            var tangent = _splineContainer.EvaluateTangent(value);
            var up = _splineContainer.EvaluateUpVector(value);

            transform.SetPositionAndRotation(position, Quaternion.LookRotation(tangent, up));
        }
    }
}