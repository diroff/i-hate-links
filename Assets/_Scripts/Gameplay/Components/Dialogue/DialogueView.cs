using Data.Dialogue;
using DG.Tweening;
using Reflex.Attributes;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class DialogueView : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _speakerText;
        [SerializeField] private TMP_Text _bodyText;
        [SerializeField] private Image _portraitImage;
        [SerializeField] private Button _nextButton;

        [Header("Animation Settings")]
        [SerializeField] private float _fadeDuration = 0.25f;
        [SerializeField] private float _timePerCharacter = 0.03f;

        [Inject] private DialogueService _dialogueService;
        private Tween _fadeTween;
        private Tween _typewriterTween;
        private bool _isTyping;
        private string _targetTextString;

        private void Awake()
        {
            _nextButton.onClick.AddListener(OnNextButtonClicked);

            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }

        private void OnEnable()
        {
            if (_dialogueService == null)
                return;

            _dialogueService.OnNodeStarted += OnNodeStarted;
            _dialogueService.OnDialogueEnded += OnDialogueEnded;
        }

        private void OnDisable()
        {
            if (_dialogueService == null)
                return;

            _dialogueService.OnNodeStarted -= OnNodeStarted;
            _dialogueService.OnDialogueEnded -= OnDialogueEnded;
        }

        private void OnNodeStarted(DialogueNode node)
        {
            if (_canvasGroup.alpha < 0.99f)
            {
                ShowView();
            }

            SetupSpeaker(node.Speaker);
            SetupText(node);
        }

        private void OnDialogueEnded()
        {
            HideView();
        }

        private void SetupSpeaker(DialogueCharacterSO speaker)
        {
            if (speaker != null)
            {
                if (_speakerText != null)
                {
                    _speakerText.text = speaker.Name.GetLocalizedString();
                }

                if (_portraitImage != null)
                {
                    _portraitImage.gameObject.SetActive(speaker.Portrait != null);
                    _portraitImage.sprite = speaker.Portrait;
                }
            }
            else
            {
                if (_speakerText != null)
                    _speakerText.text = string.Empty;

                if (_portraitImage != null)
                    _portraitImage.gameObject.SetActive(false);
            }
        }

        private void SetupText(DialogueNode node)
        {
            _typewriterTween?.Kill();

            _targetTextString = node.Text.GetLocalizedString();
            _bodyText.text = _targetTextString;
            _bodyText.maxVisibleCharacters = 0;

            int totalCharacters = _targetTextString.Length;
            float duration = totalCharacters * _timePerCharacter;

            _isTyping = true;

            _typewriterTween = DOTween.To(
                () => _bodyText.maxVisibleCharacters,
                x => _bodyText.maxVisibleCharacters = x,
                totalCharacters,
                duration
            )
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                _isTyping = false;
            });
        }

        private void CompleteTextInstantly()
        {
            _typewriterTween?.Kill();
            _bodyText.maxVisibleCharacters = _targetTextString.Length;
            _isTyping = false;
        }

        private void OnNextButtonClicked()
        {
            if (_isTyping)
                CompleteTextInstantly();
            else
                _dialogueService.Advance();
        }

        private void ShowView()
        {
            _fadeTween?.Kill();
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
            _fadeTween = _canvasGroup.DOFade(1f, _fadeDuration);
        }

        private void HideView()
        {
            _fadeTween?.Kill();
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
            _fadeTween = _canvasGroup.DOFade(0f, _fadeDuration);
        }
    }
}