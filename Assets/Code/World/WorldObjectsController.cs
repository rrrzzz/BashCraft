using System;
using System.Collections.Generic;
using Code.GameLogic;
using UnityEngine;

namespace Code.World
{
    public class WorldObjectsController
    {
        private const int DeathHealth = 0;
        private const int AliveHealth = 3;
        
        private readonly int _animHealthHash = Animator.StringToHash("Health");
        private readonly int _animAttackHash = Animator.StringToHash("Attack");
        private readonly Dictionary<int, IPlayerGameObject> _playerGameObjects = new Dictionary<int, IPlayerGameObject>();
        private readonly Material[] _playerMaterials;
        private readonly IWorldObjectsGenerator _worldObjectsGenerator;
        private readonly int _playerCount;

        public WorldObjectsController(IWorldObjectsGenerator worldObjectsGenerator, int playerCount, Material[] playerMaterials)
        {
            _playerCount = playerCount;
            _worldObjectsGenerator = worldObjectsGenerator;
            _playerMaterials = playerMaterials;
            PlayerController.PlayerAttackEvent += OnPlayerAttack;
            PlayerController.PlayerDiedEvent += OnPlayerDeath;
            GameController.GameRestartedEvent += OnGameRestarted;
            GameController.GameEndedEvent += OnGameEnded;
            InitializePlayerGameObjects();
        }

        private void InitializePlayerGameObjects()
        {
            var players = _worldObjectsGenerator.GeneratePlayerGameObjects(_playerCount);
            for (int i = 0; i < _playerCount; i++)
            {
                var player = players[i];
                var currentMaterialIndex = i % _playerMaterials.Length;
                player.Renderer.material = _playerMaterials[currentMaterialIndex];
                _playerGameObjects.Add(i, player);
            }
        }

        private void OnGameRestarted(object sender, bool isBuffed)
        {
            ResetPlayerStates();
        }

        private void OnPlayerDeath(object sender, int id)
        {
            SetHealthAnimationParam(_playerGameObjects[id], DeathHealth);
        }

        private void ResetPlayerStates()
        {
            foreach (var player in _playerGameObjects.Values)
            {
                SetHealthAnimationParam(player, AliveHealth);
            }
        }
        
        private void OnPlayerAttack(object sender, int id)
        {
            _playerGameObjects[id].PlayerAnimator.SetTrigger(_animAttackHash);
        }

        private void SetHealthAnimationParam(IPlayerGameObject player, int health)
        {
            player.PlayerAnimator.SetInteger(_animHealthHash, health);
        }
        
        private void OnGameEnded(object sender, EventArgs e)
        {
            PlayerController.PlayerAttackEvent -= OnPlayerAttack;
            PlayerController.PlayerDiedEvent -= OnPlayerDeath;
            GameController.GameRestartedEvent -= OnGameRestarted;
            GameController.GameEndedEvent -= OnGameEnded;
        }
    }
}