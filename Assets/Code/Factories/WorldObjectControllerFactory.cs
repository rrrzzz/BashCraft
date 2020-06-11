using Code.Utility;
using Code.World;
using UnityEngine;

namespace Code.Factories
{
    public class WorldObjectControllerFactory : IWorldObjectControllerFactory
    {
        public WorldObjectsController WorldObjectController { get; }
        
        public WorldObjectControllerFactory(IWorldObjectsGenerator worldObjectsGenerator,
            IResourcesRepository resourcesRepository, Material[] materials)
        {
            WorldObjectController = new WorldObjectsController(worldObjectsGenerator, 
                resourcesRepository.GameSettings.PlayersCount, materials);
        }
    }
}