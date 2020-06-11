using System.Collections.Generic;
using System.IO;
using System.Linq;
using Code.Data;
using Code.Data.GameDataTypes;
using UnityEngine;

namespace Code.Utility
{
    public class ResourcesRepository : IResourcesRepository
    {
        private const string DataFileName = "data";
        private const string PlayerPanelPrefabName = "PlayerPanel";
        private const string GameRestartPanelPrefabName = "GameRestartPanel";
        private const string StatPrefabName = "Stat";
        private const string IconsFolder = "Icons";
        private const string PrefabsFolder = "Prefabs";
        private const string PlayerModelPrefab = "PlayerModelPrefab";
        private readonly JsonDataObject _jsonGameData;
        
        public GameSettings GameSettings { get; }
        public Buff[] PlayerBuffs { get; }
        public Stat[] PlayerStats { get; }
        
        private readonly Dictionary<string, Sprite> _icons = new Dictionary<string, Sprite>();

        public ResourcesRepository()
        {
            var jsonString = Helpers.GetJsonText(DataFileName);
            _jsonGameData = JsonUtility.FromJson<JsonDataObject>(jsonString);
            GameSettings = GetGameSettings();
            PlayerBuffs = GetBuffs();
            PlayerStats = GetStats();
        }
        
        public GameObject GetStatPrefab() => Resources.Load<GameObject>(Path.Combine(PrefabsFolder, StatPrefabName));
        public GameObject GetPlayerPanelPrefab() => Resources.Load<GameObject>(Path.Combine(PrefabsFolder, PlayerPanelPrefabName));
        public GameObject GetGameRestartPanelPrefab() => Resources.Load<GameObject>(Path.Combine(PrefabsFolder, GameRestartPanelPrefabName));
        public GameObject GetPlayerModelPrefab() => Resources.Load<GameObject>(Path.Combine(PrefabsFolder, PlayerModelPrefab));
        public Sprite GetIcon(string iconName)
        {
            if (_icons.TryGetValue(iconName, out var icon)) return icon;

            icon = Resources.Load<Sprite>(Path.Combine(IconsFolder, iconName));
            _icons.Add(iconName, icon);
            return icon;
        }

        private GameSettings GetGameSettings() => DataProvider.JsonDataToGameData(_jsonGameData.settings);
        private Buff[] GetBuffs() => _jsonGameData.buffs.Select(DataProvider.JsonDataToGameData).ToArray();
        private Stat[] GetStats() => _jsonGameData.stats.Select(DataProvider.JsonDataToGameData).ToArray();
    }
}