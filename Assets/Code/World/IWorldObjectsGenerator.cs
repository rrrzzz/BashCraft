using UnityEngine;

namespace Code.World
{
    public interface IWorldObjectsGenerator
    {
        IPlayerGameObject[] GeneratePlayerGameObjects(int playerCount);
        GameObject PlayerModelPrefab { get; set; }
    }
}