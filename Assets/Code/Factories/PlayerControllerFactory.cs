using System.Collections;
using System.Collections.Generic;
using Code.Factories;
using Code.GameLogic;
using Code.UI;
using Code.Utility;
using UnityEngine;

public class PlayerControllerFactory
{
   public PlayerController PlayerController { get; }
   
   public PlayerControllerFactory(IBuffsRepository buffRepository, IResourcesRepository resourcesRepository)
   {
      PlayerController = new PlayerController(buffRepository, resourcesRepository.PlayerStats, resourcesRepository.GameSettings.PlayersCount);
   }
}
