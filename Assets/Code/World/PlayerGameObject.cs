using UnityEngine;

namespace Code.World
{
    public class PlayerGameObject : MonoBehaviour, IPlayerGameObject
    {
        public Animator PlayerAnimator { get; private set; }
        public Renderer Renderer { get; private set; }
        public Transform Transform { get; private set; }

        private void OnEnable()
        {
            PlayerAnimator = GetComponent<Animator>();
            Renderer = GetComponentInChildren<Renderer>();
            Transform = transform;
        }
    }
}
