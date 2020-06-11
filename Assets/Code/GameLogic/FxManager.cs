using UnityEngine;

namespace Code.GameLogic
{
    public class FxManager : MonoBehaviour
    {
        [SerializeField] private Shader defaultGameShader;
        [SerializeField] private Shader buffedGameShader;
        [SerializeField] private Renderer groundRenderer;
        private Material _mat;
        
        private void Start()
        {
            _mat = groundRenderer.material;
            GameController.GameRestartedEvent += OnGameRestarted;
        }

        private void OnGameRestarted(object sender, bool isBuffedGame) => _mat.shader = isBuffedGame ? buffedGameShader : defaultGameShader;

        private void OnDestroy() => GameController.GameRestartedEvent -= OnGameRestarted;
    }
}
