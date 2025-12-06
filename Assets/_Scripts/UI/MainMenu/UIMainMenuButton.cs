using Abstractions.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.MainMenu
{
    public class UIMainMenuButton : UIButtonBase
    {
        [SerializeField] private float _hoverScale = 1.08f;
        [SerializeField] private float _pressScale = 0.95f;
        [SerializeField] private float _duration = 0.15f;
        [SerializeField] private Ease _easeIn = Ease.OutQuad;
        [SerializeField] private Ease _easeOut = Ease.OutBack;

        private Vector3 _initialPosition;
        private Vector3 _initialScale;

        private Sequence _hoverSequence;
        private Tween _pressTween;

        protected override void Awake()
        {
            base.Awake();

            _initialPosition = transform.position;
            _initialScale = transform.localScale;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _hoverSequence?.Kill();
            _pressTween?.Kill();

            transform.DOKill();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            _hoverSequence?.Kill();
            _hoverSequence = DOTween.Sequence()
                .Append(transform.DOScale(_hoverScale, _duration).SetEase(_easeOut))
                .Play();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            _hoverSequence?.Kill();
            _hoverSequence = DOTween.Sequence()
                .Append(transform.DOScale(_initialScale, _duration).SetEase(_easeIn))
                .Play();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            _pressTween?.Kill();
            _pressTween = transform.DOScale(_pressScale, 0.08f).SetEase(Ease.OutCubic);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            _pressTween?.Kill();
            transform.DOScale(_hoverScale, 0.1f);
        }
    }
}