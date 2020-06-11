using System.Collections.Generic;
using Code.Data.GameDataTypes;
using Code.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class UiElementsGenerator : MonoBehaviour, IUiElementsGenerator
    {
        private const string MainCanvasTag = "MainCanvas";
        private const string UiPlayerName = "Player";
        
        private Transform _canvas;
        private GameObject _playerPanelPrefab;
        private GameObject _restartGamePanelPrefab;
        private GameObject _statPrefab;
        private IResourcesRepository _resourcesRepository;
        private bool _isPanelOnRightSide;
  
        public void InitializeGenerator(IResourcesRepository resourcesRepository)
        {
            _canvas = GameObject.FindGameObjectWithTag(MainCanvasTag).transform;
            _playerPanelPrefab = resourcesRepository.GetPlayerPanelPrefab();
            _restartGamePanelPrefab = resourcesRepository.GetGameRestartPanelPrefab();
            _statPrefab = resourcesRepository.GetStatPrefab();
            _resourcesRepository = resourcesRepository;
        }

        public Button[] CreateAndGetGameRestartButtons()
        {
            var go = Instantiate(_restartGamePanelPrefab, _canvas.transform);
            return go.GetComponentsInChildren<Button>();
        }

        public PlayerCharacteristicUi GenerateStatUiElement(Transform parent, Stat stat)
        {
            var icon = _resourcesRepository.GetIcon(stat.Icon);
            var go = Instantiate(_statPrefab, parent);
            var uiScript = go.AddComponent<PlayerCharacteristicUi>();
            uiScript.Icon.sprite = icon;
            return uiScript;
        }

        public PlayerCharacteristicUi GenerateBuffUiElement(Transform parent, Buff buff)
        {
            var icon = _resourcesRepository.GetIcon(buff.Icon);
            var go = Instantiate(_statPrefab, parent);
            var uiScript = go.AddComponent<PlayerCharacteristicUi>();
            uiScript.Icon.sprite = icon;
            uiScript.Text.text = buff.Title;
            return uiScript;
        }

        public IPlayerUi GeneratePlayerUiObject(int id)
        {
            var go = Instantiate(_playerPanelPrefab, _canvas.transform);
            go.name = $"{UiPlayerName}{id}";
            return go.AddComponent<PlayerUi>();
        }
    }
}