using System.Collections.Generic;
using Code.Data.GameDataTypes;

namespace Code.GameLogic
{
    public interface IPlayerObject
    {
         Dictionary<StatType, int> Stats { get; }
         bool IsDead { get; set; }
         IPlayerObject CurrentEnemy { get; set; }
         int Id { get; }
    }
}