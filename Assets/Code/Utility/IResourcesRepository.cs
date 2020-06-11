using System.Collections.Generic;
using Code.Data.GameDataTypes;
using UnityEngine;

namespace Code.Utility
{
    public interface IResourcesRepository
    {
         GameSettings GameSettings { get; }
         Buff[] PlayerBuffs { get; }
         Stat[] PlayerStats { get; }
         GameObject GetStatPrefab();
         GameObject GetPlayerPanelPrefab();
         GameObject GetGameRestartPanelPrefab();
         GameObject GetPlayerModelPrefab();
         Sprite GetIcon(string iconName);
    }
}