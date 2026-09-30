using Abstractions.Interfaces;
using Gameplay.Components.Interaction.Doors;
using Reflex.Core;
using UnityEngine;

namespace Installers
{
    public class Level1Installer : BaseLevelInstaller
    {
        [SerializeField] private BaseDoor _baseDoor;

        protected override void InstallFeatureInstallers(ContainerBuilder containerBuilder)
        {
            base.InstallFeatureInstallers(containerBuilder);

            containerBuilder.AddSingleton(_baseDoor, typeof(ILevelCompleter));
        }
    }
}