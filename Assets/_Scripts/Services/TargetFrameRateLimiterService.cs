using UnityEngine;

namespace Services
{
    public class TargetFrameRateLimiterService : MonoBehaviour
    {
        [SerializeField] private int _targetFrameRate = 60;

        private void Update()
        {
            Application.targetFrameRate = _targetFrameRate;
        }
    }
}