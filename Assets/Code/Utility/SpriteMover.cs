using System.Collections;
using UnityEngine;

namespace Code.Utility
{
    public class SpriteMover : MonoBehaviour
    {
        [SerializeField]
        private Transform reference;
        [SerializeField]
        private float timeToMove = 0.6f;
        
        private Vector3 _referencePos;
        private Vector3 _velocity;
        
        private void Start()
        {
            _referencePos = reference.position;
            StartCoroutine(MoveToPos());
        }
    
        private IEnumerator MoveToPos()
        {
            while ((transform.position - _referencePos).magnitude > 0.4f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, _referencePos, ref _velocity, timeToMove);
                yield return null;
            }

            transform.position = _referencePos;
        }
    }
}