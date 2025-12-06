using Reflex.Core;
using Services;
using UnityEngine;

namespace Installers
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private string _menuSceneName;

        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(new SceneManagerService(_menuSceneName));
        }
    }
}