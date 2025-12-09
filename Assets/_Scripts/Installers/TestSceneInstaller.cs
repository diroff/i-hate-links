using Reflex.Core;
using Testers;
using UnityEngine;

namespace Installers
{
    public class TestSceneInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            var key = FindAnyObjectByType<TestKey>();

            if (key != null)
                containerBuilder.AddSingleton(key);
        }
    }
}