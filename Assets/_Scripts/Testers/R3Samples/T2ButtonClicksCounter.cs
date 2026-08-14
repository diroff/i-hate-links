using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Testers.R3Samples
{
    public class T2ButtonClicksCounter : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button
                .OnClickAsObservable()
                .Scan(0, (count, _) => count + 1)
                .Where(x => x % 3 == 0)
                .Subscribe(count => Debug.Log($"{count}"))
                .AddTo(gameObject);
            
        }
    }
}