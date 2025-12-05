using Abstractions.Interfaces;
using Cysharp.Threading.Tasks;
using Reflex.Core;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;

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