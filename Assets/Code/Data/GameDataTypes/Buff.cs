using System;

namespace Code.Data.GameDataTypes
{
    [Serializable]
    public struct Buff
    {
        public BuffType BuffType { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public BuffStat[] BuffStats { get; set; }
    }
}
