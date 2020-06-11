using System.Collections.Generic;
using System.Linq;
using Code.Data.GameDataTypes;
using Random = UnityEngine.Random;

namespace Code.GameLogic
{
    public class BuffsRepository : IBuffsRepository
    {
        public Dictionary<BuffType, BuffStat[]> BuffsStats { get; } = new Dictionary<BuffType, BuffStat[]>();
        private readonly Buff[] _buffs;
        private readonly int _buffTypesCount;
        private readonly GameSettings _gameSettings;

        public BuffsRepository(Buff[] buffs, GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            foreach (var buff in buffs)
            {
                BuffsStats.Add(buff.BuffType, buff.BuffStats);
                _buffs = buffs;
            }
            
            _buffTypesCount = _buffs.Length;
        }
    
        public Buff[] GetRandomBuffs()
        {
            var randomBuffCount = Random.Range(_gameSettings.BuffCountMin, _gameSettings.BuffCountMax + 1);
        
            if (_gameSettings.AllowBuffStacking)
            {
                var buffArray = new Buff[randomBuffCount];
                for (int i = 0; i < randomBuffCount; i++) buffArray[i] = _buffs[GetRandomBuffId()];
                return buffArray;
            }

            if (randomBuffCount >= _buffTypesCount) return _buffs;
            
            var idsSet = new HashSet<int>();
            while (idsSet.Count != randomBuffCount)
            {
                idsSet.Add(GetRandomBuffId());
            }

            return idsSet.Select(x => _buffs[x]).ToArray();
        }
        
        private int GetRandomBuffId() => Random.Range(0, _buffTypesCount);
    }
}