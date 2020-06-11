namespace Code.Data.GameDataTypes
{
    public struct Stat
    {
        public StatType StatType { get; }
        public string Title { get; }
        public int Value { get; }
        public string Icon { get; }
        
        public Stat(StatType statType, string title, int value, string icon)
        {
            StatType = statType;
            Title = title;
            Value = value;
            Icon = icon;
        }
    }
}