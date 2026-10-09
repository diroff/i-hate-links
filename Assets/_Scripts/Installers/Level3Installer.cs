using Abstractions.Interfaces;
using Gameplay.Components.Interaction.Doors;
using Gameplay.Mechanics.Interactables;
using Reflex.Core;
using UnityEngine;

namespace Installers
{
    public class Level3Installer : BaseLevelInstaller
    {
        [SerializeField] private BaseDoor _baseDoor;
        [SerializeField] private PowerSwitch _powerSwitch;

        protected override void InstallFeatureInstallers(ContainerBuilder containerBuilder)
        {
            base.InstallFeatureInstallers(containerBuilder);

            containerBuilder.AddSingleton(_baseDoor, typeof(ILevelCompleter));
            containerBuilder.AddSingleton(_powerSwitch);
        }
    }
}