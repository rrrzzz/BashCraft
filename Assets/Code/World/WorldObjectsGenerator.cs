using System;
using UnityEngine;

namespace Code.World
{
    public class WorldObjectsGenerator : MonoBehaviour, IWorldObjectsGenerator
    {
        public GameObject PlayerModelPrefab { get; set; }
        
        private const float Rotation = 180;
        private const string PlayerObjectName = "Player";
        
        [SerializeField] private Transform playersRoot;
        [SerializeField] private Transform initialSpawnPosition;
        [SerializeField] private float playersDistance;
        [SerializeField] private float spawnPositionOffset;
        
        private Vector3 _currentSpawnPosition;
        
        public IPlayerGameObject[] GeneratePlayerGameObjects(int playerCount)
        {
            _currentSpawnPosition = initialSpawnPosition.position;
            if (playerCount % 2 != 0) throw new ArgumentException($"Player count must be even but is {playerCount}");
            
            var playerObjects = new IPlayerGameObject[playerCount];
            for (int i = 0; i < playerCount; i += 2)
            {
                var playerOne = InstantiatePlayer(i);
                var playerTwo = InstantiatePlayer(i + 1);
                
                OffsetAndRotatePlayer(playerOne.Transform, -playersDistance, Rotation);
                OffsetAndRotatePlayer(playerTwo.Transform, playersDistance);
                playerObjects[i] = playerOne;
                playerObjects[i + 1] = playerTwo;
                
                _currentSpawnPosition -= playerOne.Transform.right * spawnPositionOffset;
            }

            return playerObjects;
        }

        private IPlayerGameObject InstantiatePlayer(int id)
        {
           var go = Instantiate(PlayerModelPrefab, playersRoot);
           go.transform.position = _currentSpawnPosition;
           go.name = $"{PlayerObjectName}{id}";
           return go.AddComponent<PlayerGameObject>();
        } 

        private void OffsetAndRotatePlayer(Transform playerTransform, float distance, float rotation = 0)
        {
            var offset = playerTransform.forward * -distance;
            playerTransform.position += offset;

            if (Math.Abs(rotation) > 0.01)
            {
                playerTransform.Rotate(playerTransform.up, rotation);
            }
        }
    }
}