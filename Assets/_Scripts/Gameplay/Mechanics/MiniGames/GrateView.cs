using DG.Tweening;
using UnityEngine;

namespace Gameplay.Mechanics.MiniGames
{
    public class GrateView : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private GrateMiniGame _grateMiniGame;

        [Header("Components")]
        [SerializeField] private SpriteRenderer _grateSpriteRenderer;

        [Header("Movement Bounds")]
        [SerializeField] private Vector2 _targetOpenPosition;
        [SerializeField] private float _smoothDuration = 0.15f;

        private Vector3 _startPosition;
        private Tweener _moveTweener;

        private void Awake()
        {
            if (_grateSpriteRenderer == null)
                _grateSpriteRenderer = GetComponent<SpriteRenderer>();

            _startPosition = _grateSpriteRenderer.transform.localPosition;
        }

        private void OnEnable()
        {
            if (_grateMiniGame != null && _grateMiniGame.QteComponent != null)
                _grateMiniGame.QteComponent.OnProgressChanged += UpdateProgress;
        }

        private void OnDisable()
        {
            if (_grateMiniGame != null && _grateMiniGame.QteComponent != null)
                _grateMiniGame.QteComponent.OnProgressChanged -= UpdateProgress;
        }

        private void OnDestroy()
        {
            _moveTweener?.Kill();
        }

        public void UpdateProgress(float progress)
        {
            Vector3 calculatedTarget = Vector3.Lerp(_startPosition, _targetOpenPosition, Mathf.Clamp01(progress));

            _moveTweener?.Kill();
            _moveTweener = _grateSpriteRenderer.transform
                .DOLocalMove(calculatedTarget, _smoothDuration)
                .SetEase(Ease.OutQuad);
        }
    }
}