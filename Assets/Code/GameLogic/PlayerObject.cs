using System.Collections.Generic;
using Code.Data.GameDataTypes;

namespace Code.GameLogic
{
    public class PlayerObject : IPlayerObject
    {
        public Dictionary<StatType, int> Stats { get; } = new Dictionary<StatType, int>();
        public bool IsDead { get; set; }
        public IPlayerObject CurrentEnemy { get; set; }
        public int Id { get; }

        public PlayerObject(int id)
        {
            Id = id;
        }
    }
}