using Gameplay.Mechanics.Player;
using Reflex.Core;
using UnityEngine;

namespace Installers
{
    public class BaseLevelInstaller : MonoBehaviour, IInstaller
    {
        [Header("Base Level References")]
        [SerializeField] private Player _playerInstance;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(_playerInstance);

            InstallFeatureInstallers(containerBuilder);
        }

        protected virtual void InstallFeatureInstallers(ContainerBuilder containerBuilder)
        {

        }
    }
}