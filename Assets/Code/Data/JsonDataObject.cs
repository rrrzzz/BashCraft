using System;
using Code.Data.JsonDataTypes;

namespace Code.Data
{
   [Serializable]
   public struct JsonDataObject
   {
      public SettingsData settings;
      public StatData[] stats;
      public BuffData[] buffs;
   }
}
