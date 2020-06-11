using Code.World;

namespace Code.Factories
{
    public interface IWorldObjectControllerFactory
    {
        WorldObjectsController WorldObjectController { get; }
    }
}