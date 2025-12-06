using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using Services;
using System.Threading;

namespace UI.MainMenu
{
    public class UIExitButton : UIMainMenuButton
    {
        [Inject] private SceneManagerService _sceneManager;

        protected override async UniTask OnClickAsync(CancellationToken ct)
        {
            SetInteractable(false);

            _sceneManager.QuitGame();

            await UniTask.Delay(100);

            SetInteractable(true);
        }
    }
}