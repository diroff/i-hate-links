using Abstractions.Components.Inventory;
using Abstractions.Interfaces;
using Data;
using DG.Tweening;
using UnityEngine;

namespace Testers
{
    public class TestChest : MonoBehaviour, IInteractable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _openedSprite;
        [SerializeField] private Sprite _closedSprite;

        [SerializeField] private ItemDefinition _requiredItem;
        [SerializeField] private Rigidbody2D _coinPrefab;
        [SerializeField] private int _coinsLimit = 15;

        [SerializeField] private float _spawnForce = 5f;
        [SerializeField] private float _spawnRadius = 0.5f;
        [SerializeField] private float _torqueForce = 300f;

        private bool _isOpened = false;

        private int _spawnedCoinsCount;

        public bool CanInteract(GameObject interactor) => _spawnedCoinsCount <= _coinsLimit;

        private void Awake()
        {
            _spriteRenderer.sprite = _closedSprite;
        }

        public void Interact(GameObject interactor)
        {
            if (!_isOpened)
                TryToOpenChest(interactor);
            else
                LootChest();
        }

        private void TryToOpenChest(GameObject interactor)
        {
            interactor.TryGetComponent(out InventoryComponent inventory);

            if (inventory == null)
            {
                Debug.Log("<color=red>У тебя даже карманов нет!</color>");
                return;
            }

            if (!inventory.HasItem(_requiredItem))
            {
                Debug.Log("<color=red>Нужен ключ!</color>");
                return;
            }

            inventory.Remove(_requiredItem);

            _isOpened = true;
            _spriteRenderer.sprite = _openedSprite;
            Debug.Log("<color=green>Сундук открыт!</color>");
        }

        private void LootChest()
        {
            if (_spawnedCoinsCount >= _coinsLimit)
            {
                DestroyChest();
                return;
            }

            _spawnedCoinsCount++;

            Vector2 randomOffset = Random.insideUnitCircle * _spawnRadius;
            Vector3 spawnPos = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0f);

            var coin = Instantiate(_coinPrefab, spawnPos, Quaternion.identity);

            Vector2 forceDirection = Random.insideUnitCircle.normalized;
            float force = _spawnForce + Random.Range(-1f, 2f);

            coin.linearVelocity = forceDirection * force;

            float torque = Random.Range(-_torqueForce, _torqueForce);
            coin.angularVelocity = torque;

            Destroy(coin, 5f);
        }

        private void DestroyChest()
        {
            if (TryGetComponent(out Collider2D collider))
                collider.enabled = false;

            var sequence = DOTween.Sequence();

            sequence.Append(transform.DOPunchScale(Vector3.one * 1.5f, 0.6f * 0.6f, 10, 1f))
                    .Join(transform.DOPunchRotation(new Vector3(0, 0, 360f * 3), 0.6f, 15, 0.5f))
                    .Join(_spriteRenderer.DOFade(0f, 0.4f))
                    .OnComplete(() =>
                    {
                        _spriteRenderer.enabled = false;
                        Debug.Log("Отлично, теперь не осталось даже сундука");
                    });
        }
    }
}