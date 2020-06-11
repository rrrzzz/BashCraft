using System.Linq;
using Code.Data.GameDataTypes;
using Code.Data.JsonDataTypes;

namespace Code.Data
{
    public static class DataProvider
    {
        public static Buff JsonDataToGameData(BuffData data)
        {
            var buffStruct = new Buff
            {
                BuffType = (BuffType)data.id,
                Title = data.title,
                Icon = data.icon,
                BuffStats = data.stats.Select(JsonDataToGameData).ToArray()
            };

            return buffStruct;
        }

        public static Stat JsonDataToGameData(StatData data)
        {
            var statStruct = new Stat
            (
                (StatType)data.id,
                data.title,
                data.value,
                data.icon
            );
            
            return statStruct;
        }
        
        public static GameSettings JsonDataToGameData(SettingsData data)
        {
            var settingsStruct = new GameSettings
            (
                data.allowDuplicateBuffs,
                data.playersCount,
                data.buffCountMin,
                data.buffCountMax
            );
            
            return settingsStruct;
        }
        
        private static BuffStat JsonDataToGameData(BuffStatData data)
        {
            var buffStatStruct = new BuffStat
            (
                (StatType)data.statId,
                data.value 
            );
            
            return buffStatStruct;
        }
    }
}
