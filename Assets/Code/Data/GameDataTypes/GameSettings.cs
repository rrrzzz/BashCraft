namespace Code.Data.GameDataTypes
{
    public struct GameSettings
    {
        public int PlayersCount { get; }
        public int BuffCountMin { get; }
        public int BuffCountMax { get; }
        public bool AllowBuffStacking { get; }
        
        public GameSettings(bool allowBuffStacking, int playersCount, int buffCountMin, int buffCountMax)
        {
            AllowBuffStacking = allowBuffStacking;
            PlayersCount = playersCount;
            BuffCountMin = buffCountMin;
            BuffCountMax = buffCountMax;
        }
    }
}
