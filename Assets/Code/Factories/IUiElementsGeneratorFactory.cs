using Code.UI;

namespace Code.Factories
{
    public interface IUiGeneratorFactory
    {
        IUiElementsGenerator UiElementsGenerator { get; }
    }
}