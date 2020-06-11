using System.Collections.Generic;
using Code.Data.GameDataTypes;

namespace Code.GameLogic
{
    public interface IBuffsRepository
    {
        Dictionary<BuffType, BuffStat[]> BuffsStats { get; }
        Buff[] GetRandomBuffs();
    }
}