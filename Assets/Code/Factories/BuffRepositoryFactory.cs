using Code.GameLogic;
using Code.Utility;

namespace Code.Factories
{
    public class BuffRepositoryFactory : IBuffRepositoryFactory
    {
        public IBuffsRepository BuffsRepository { get; }

        public BuffRepositoryFactory(IResourcesRepository resourcesRepository)
        {
            var settings = resourcesRepository.GameSettings;
            var playerBuffs = resourcesRepository.PlayerBuffs;
            BuffsRepository = new BuffsRepository(playerBuffs, settings);
        }
    }
}