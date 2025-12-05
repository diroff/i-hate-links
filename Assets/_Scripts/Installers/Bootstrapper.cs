using Abstractions.Interfaces;
using Reflex.Attributes;
using Reflex.Core;
using Services;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [Inject] private Container _container;

    private async void Awake()
    {
        var services = _container.All<IService>();

        foreach (var service in services)
            await service.Initialize();

        var sceneManager = _container.Resolve<SceneManagerService>();
        await sceneManager.LoadMenuSceneAsync();
    }
}