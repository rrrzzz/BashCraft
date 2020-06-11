namespace Code.Data.GameDataTypes
{
    public struct BuffStat
    {
        public StatType StatType { get; }
        public int Value { get; }

        public BuffStat(StatType statType, int value)
        {
            StatType = statType;
            Value = value;
        }
    }
}