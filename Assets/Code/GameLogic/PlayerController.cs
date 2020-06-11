using System;
using System.Collections.Generic;
using Code.Data.GameDataTypes;
using Code.UI;
using UnityEngine;

namespace Code.GameLogic
{
    public class PlayerController
    {
        public static event EventHandler<PlayerBuffAddedEventArgs> PlayerBuffAddedEvent;
        public static event EventHandler<PlayerStatChangedEventArgs> PlayerStatChangedEvent;
        public static event EventHandler<int> PlayerDiedEvent;
        public static event EventHandler<int> PlayerAttackEvent;
        
        private const int DefaultStatValue = 0;
        private const int StatsMax = 100;
        
        private readonly Stat[] _defaultStats;
        private readonly IBuffsRepository _buffsRepository;
        private readonly int _playerCount;
        
        private Dictionary<int, IPlayerObject> _playerObjects = new Dictionary<int, IPlayerObject>();
        
        public PlayerController(IBuffsRepository buffsRepository, Stat[] defaultStats, int playerCount)
        {
            _playerCount = playerCount;
            _defaultStats = defaultStats;
            _buffsRepository = buffsRepository;
            GameController.GameRestartedEvent += OnGameRestartedHandler;
            GameController.GameEndedEvent += OnGameEnded;
            PlayerUiController.AttackEvent += OnAttack;
        }

        private void OnGameRestartedHandler(object sender, bool isBuffGame) => InitializePlayerObjects(isBuffGame);
   
        private void InitializePlayerObjects(bool isBuffGame)
        {
            _playerObjects = new Dictionary<int, IPlayerObject>();
            for (int i = 0; i < _playerCount; i++)
            {
                IPlayerObject playerObject = new PlayerObject(i);
                _playerObjects.Add(i, playerObject);
            
                foreach (var defaultStat in _defaultStats)
                {
                    playerObject.Stats.Add(defaultStat.StatType, DefaultStatValue);
                    SetStat(i, defaultStat.StatType, defaultStat.Value);
                }
            
                if (isBuffGame)
                {
                    var buffs = _buffsRepository.GetRandomBuffs();
                    ApplyBuffs(i, buffs);
                }
            }

            CreateEnemyPairs();
        }

        private void CreateEnemyPairs()
        {
            for (int i = 0; i < _playerCount; i += 2)
            {
                SetEnemies(i, i + 1);
            }
        }

        private void ApplyBuffs(int id, params Buff[] buffs)
        {
            foreach (var buff in buffs)
            {
                var buffStats = _buffsRepository.BuffsStats[buff.BuffType];
                PlayerBuffAddedEvent?.Invoke(this, new PlayerBuffAddedEventArgs(buff, id));
            
                foreach (var stat in buffStats)
                {
                    AddStat(id, stat.StatType, stat.Value);
                }
            }
        }

        private void SetEnemies(int firstEnemyId, int secondEnemyId)
        {
            var firstEnemy = GetPlayer(firstEnemyId);
            var secondEnemy = GetPlayer(secondEnemyId);

            firstEnemy.CurrentEnemy = secondEnemy;
            secondEnemy.CurrentEnemy = firstEnemy;
        }
    
        private void SetStat(int id, StatType type, int value)
        {
            var playerObject = GetPlayer(id);
            playerObject.Stats[type] = value;
            PlayerStatChangedEvent?.Invoke(this, new PlayerStatChangedEventArgs(type, value, id));
        }

        private void AddStat(int id, StatType type, int value)
        {
            var playerObject = GetPlayer(id);
            playerObject.Stats[type] += value;
            PlayerStatChangedEvent?.Invoke(this, new PlayerStatChangedEventArgs(type, playerObject.Stats[type], id));
        }
    
        private IPlayerObject GetPlayer(int id) => _playerObjects[id];

        private void OnAttack(object sender, int id)
        {
            PlayerAttackEvent?.Invoke(this, id);
            var attacker = GetPlayer(id);
            var defender = attacker.CurrentEnemy;
            if (defender.IsDead) return;
        
            var actualDamage = CalculateActualDamage(defender, attacker.Stats[StatType.Damage]);
            var vampireBonus = CalculateVampireBonus(attacker, actualDamage);

            AddStat(defender.Id, StatType.Health, -actualDamage);
            AddStat(attacker.Id, StatType.Health, vampireBonus);
            CheckPlayerDied(defender);
        }

        private void CheckPlayerDied(IPlayerObject playerObject)
        {
            var isDead = playerObject.Stats[StatType.Health] < 1;
            if (isDead)
            {
                SetStat(playerObject.Id, StatType.Health, 0);
                playerObject.IsDead = true;
                PlayerDiedEvent?.Invoke(this, playerObject.Id);
            }
        }

        private int CalculateActualDamage(IPlayerObject defender, int damage) => 
            Mathf.RoundToInt((1.0f - (float)defender.Stats[StatType.Armor] / StatsMax) * damage);

        private int CalculateVampireBonus(IPlayerObject attacker, int actualDamage) => 
            Mathf.RoundToInt((float)attacker.Stats[StatType.Vampirism] / StatsMax * actualDamage);
    
        private void OnGameEnded(object sender, EventArgs e)
        {
            GameController.GameRestartedEvent -= OnGameRestartedHandler;
            GameController.GameEndedEvent -= OnGameEnded;
            PlayerUiController.AttackEvent -= OnAttack;
        }
    }
}