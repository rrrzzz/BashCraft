using System;

namespace Code.Data.JsonDataTypes
{
    [Serializable]
    public struct SettingsData
    {
        public int playersCount;
        public int buffCountMin;
        public int buffCountMax;
        public bool allowDuplicateBuffs;
    }
}