using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using Services;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace UI.MainMenu
{
    public class UILastLevelLoaderButton : UIMainMenuButton
    {
        [SerializeField] private string _levelName; //TODO: add level name getting from saved data

        [Inject] private SceneManagerService _sceneManager;

        protected override async UniTask OnClickAsync(CancellationToken ct)
        {
            SetInteractable(false);
            await _sceneManager.LoadSceneAsync(_levelName);
        }
    }
}