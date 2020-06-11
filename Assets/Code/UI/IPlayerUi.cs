using System.Collections.Generic;
using Code.Data.GameDataTypes;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public interface IPlayerUi
    {
         int Id { get; set; }
         Transform StatsRootTransform { get; }
         RectTransform RectTransform { get; }
         Button AttackBtn { get; }
         Dictionary<StatType, PlayerCharacteristicUi> PlayerUiStats { get;} 
         Dictionary<BuffType, List<PlayerCharacteristicUi>> PlayerBuffs { get;}
         void DisableAttackBtnForTime(float delay);
         void EnableAttackBtn(bool isEnabled);
    }
}