using Code.UI;
using Code.Utility;
using UnityEngine;

namespace Code.Factories
{
    public class UiGeneratorFactory : IUiGeneratorFactory
    {
        public IUiElementsGenerator UiElementsGenerator { get; }

        public UiGeneratorFactory(GameObject gameObject, IResourcesRepository resourcesRepository)
        {
            UiElementsGenerator = gameObject.AddComponent<UiElementsGenerator>();
            UiElementsGenerator.InitializeGenerator(resourcesRepository);
        }
    }
}