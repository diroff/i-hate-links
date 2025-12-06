using Abstractions.UI;
using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using Services;
using System.Threading;

namespace UI.Gameplay
{
    public class UIMenuLoaderButton : UIButtonBase
    {
        [Inject] private SceneManagerService _sceneManager;

        protected override async UniTask OnClickAsync(CancellationToken cancellationToken)
        {
            SetInteractable(false);

            await _sceneManager.LoadMenuSceneAsync();
        }
    }
}