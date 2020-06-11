using UnityEngine;

namespace Code.World
{
    public interface IPlayerGameObject
    {
         Animator PlayerAnimator { get; }
         Renderer Renderer { get; }
         Transform Transform { get; }
    }
}