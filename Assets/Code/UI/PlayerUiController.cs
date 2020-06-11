using System;
using System.Collections.Generic;
using Code.Data.GameDataTypes;
using Code.GameLogic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.UI
{
    public class PlayerUiController
    {
        public static event EventHandler<int> AttackEvent;
        
        private const float AttackDelay = 0.4f;
        private readonly Stat[] _defaultStats;
        private readonly Buff[] _defaultBuffs;
        private readonly int _playerCount;
        private readonly IUiElementsGenerator _uiGenerator;
        private Dictionary<int, IPlayerUi> _playerUis;
        
        public PlayerUiController(Stat[] stats, Buff[] buffs, int playerCount, IUiElementsGenerator uiGenerator)
        {
            _uiGenerator = uiGenerator;
            _defaultStats = stats;
            _defaultBuffs = buffs;
            _playerCount = playerCount;
            PlayerController.PlayerBuffAddedEvent += OnPlayerBuffAdded;
            PlayerController.PlayerStatChangedEvent += OnPlayerStatChanged;
            PlayerController.PlayerDiedEvent += OnPlayerDied;
            GameController.GameRestartedEvent += OnGameRestarted;
            GameController.GameEndedEvent += OnGameEnded;
            InitializePlayerUis();
        }
        
        private void InitializePlayerUis()
        {
            _playerUis = new Dictionary<int, IPlayerUi>();
            
            var isPanelPlacedOnRightSide = false;
            
            for (int i = 0; i < _playerCount; i++)
            {
                var playerUi = _uiGenerator.GeneratePlayerUiObject(i);
                playerUi.Id = i;
                
                playerUi.AttackBtn.onClick.AddListener(() => OnAttackBtnClicked(playerUi));
                
                SetPlayerPanelSide(playerUi.RectTransform, isPanelPlacedOnRightSide);
                isPanelPlacedOnRightSide = !isPanelPlacedOnRightSide;
                
                var root = playerUi.StatsRootTransform;
                foreach (var stat in _defaultStats)
                {
                    var statUiElement = _uiGenerator.GenerateStatUiElement(root, stat);
                    playerUi.PlayerUiStats[stat.StatType] = statUiElement;
                }
                
                foreach (var buff in _defaultBuffs)
                {
                    playerUi.PlayerBuffs.Add(buff.BuffType, new List<PlayerCharacteristicUi>());
                }
                
                _playerUis.Add(i, playerUi);
            }
        }

        private void OnGameRestarted(object sender, bool isBufFGame)
        {
            ClearBuffsAndEnableAttack();
        }

        private void OnAttackBtnClicked(IPlayerUi playerUi)
        {
            playerUi.DisableAttackBtnForTime(AttackDelay);
            AttackEvent?.Invoke(this, playerUi.Id);
        }

        private void OnPlayerDied(object sender, int id) => _playerUis[id].EnableAttackBtn(false);

        private void ClearBuffsAndEnableAttack()
        {
            foreach (var playerUi in _playerUis.Values)
            {
                playerUi.EnableAttackBtn(true);
                var buffObjectLists = playerUi.PlayerBuffs.Values;
                foreach (var list in buffObjectLists)
                {
                    list.ForEach(buffUi => Object.Destroy(buffUi.gameObject));
                }
                    
                foreach (var buff in _defaultBuffs)
                {
                    playerUi.PlayerBuffs[buff.BuffType] = new List<PlayerCharacteristicUi>();
                }
            }
        }
        
        private void OnPlayerStatChanged(object sender, PlayerStatChangedEventArgs eventArgs)
        {
            var playerUi = _playerUis[eventArgs.Id];
            playerUi.PlayerUiStats[eventArgs.StatType].Text.text = eventArgs.Value.ToString();
        }

        private void OnPlayerBuffAdded(object sender, PlayerBuffAddedEventArgs eventArgs)
        {
            var playerUi = _playerUis[eventArgs.Id];
            var root = playerUi.StatsRootTransform;
            var buffUi = _uiGenerator.GenerateBuffUiElement(root, eventArgs.Buff);
            playerUi.PlayerBuffs[eventArgs.Buff.BuffType].Add(buffUi);
        }

        private void SetPlayerPanelSide(RectTransform panelRect, bool isPanelOnRightSide)
        {
            if (isPanelOnRightSide)
            {
                panelRect.anchorMax = new Vector2(1,1);
                panelRect.anchorMin = new Vector2(1,0);
                panelRect.pivot = new Vector2(1, 0.5f);
            }
            else panelRect.pivot = new Vector2(0, 0.5f);
        }
        
        private void OnGameEnded(object sender, EventArgs e)
        {
            foreach (var playerUi in _playerUis.Values)
            {
                playerUi.AttackBtn.onClick.RemoveAllListeners();
            }
            PlayerController.PlayerBuffAddedEvent -= OnPlayerBuffAdded;
            PlayerController.PlayerStatChangedEvent -= OnPlayerStatChanged;
            PlayerController.PlayerDiedEvent -= OnPlayerDied;
            GameController.GameRestartedEvent -= OnGameRestarted;
            GameController.GameEndedEvent -= OnGameEnded;
        }
    }
}