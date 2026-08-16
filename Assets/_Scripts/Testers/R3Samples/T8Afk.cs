using R3;
using System;
using UnityEngine;

namespace Testers.R3Samples
{
    public class T8Afk : MonoBehaviour
    {
        [SerializeField] private T8Player _player;
        [SerializeField] private float _afkDelay = 3f;

        private void Start()
        {
            _player.CurrentPosition
                .DistinctUntilChanged()
                .Debounce(TimeSpan.FromSeconds(_afkDelay))
                .Subscribe(_ => Debug.Log($"Player {_player.gameObject.name} is afk"))
                .AddTo(gameObject);
        }
    }
}