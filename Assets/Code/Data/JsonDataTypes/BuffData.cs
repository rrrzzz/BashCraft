using System;

namespace Code.Data.JsonDataTypes
{
    [Serializable]
    public struct BuffData
    {
        public int id;
        public string title;
        public string icon;
        public BuffStatData[] stats;
    }
}