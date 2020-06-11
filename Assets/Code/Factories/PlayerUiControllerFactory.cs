using Code.UI;
using Code.Utility;

namespace Code.Factories
{
    public class PlayerUiControllerFactory : IPlayerUiControllerFactory
    {
        public PlayerUiController PlayerUiController { get;}
        public PlayerUiControllerFactory(IResourcesRepository resourcesRepository, IUiElementsGenerator uiElementsGenerator)
        {
            PlayerUiController = new PlayerUiController(resourcesRepository.PlayerStats, resourcesRepository.PlayerBuffs,
                resourcesRepository.GameSettings.PlayersCount, uiElementsGenerator);
        }
    }
}