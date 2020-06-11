using System;
using Code.Data.GameDataTypes;

namespace Code.GameLogic
{
    public struct PlayerStatChangedEventArgs
    {
        public StatType StatType { get; }
        public int Value { get; }
        public int Id { get; }

        public PlayerStatChangedEventArgs(StatType type, int value, int id)
        {
            StatType = type;
            Value = value;
            Id = id;
        }
    }
}