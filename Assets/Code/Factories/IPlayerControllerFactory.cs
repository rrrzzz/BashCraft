using Code.GameLogic;

namespace Code.Factories
{
    public interface IPlayerControllerFactory
    {
        PlayerController PlayerController { get; }
    }
}