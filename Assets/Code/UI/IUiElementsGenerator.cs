using Code.Data.GameDataTypes;
using Code.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public interface IUiElementsGenerator
    { 
        void InitializeGenerator(IResourcesRepository resourcesRepository);
        Button[] CreateAndGetGameRestartButtons();
        PlayerCharacteristicUi GenerateStatUiElement(Transform parent, Stat stat);
        PlayerCharacteristicUi GenerateBuffUiElement(Transform parent, Buff buff);
        IPlayerUi GeneratePlayerUiObject(int id);
    }
}