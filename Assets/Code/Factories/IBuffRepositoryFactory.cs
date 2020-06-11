using Code.GameLogic;

namespace Code.Factories
{
    public interface IBuffRepositoryFactory
    {
        IBuffsRepository BuffsRepository { get; }
    }
}