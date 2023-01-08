using System;
using UnityEngine;

namespace Amazeit.Utilities
{
    public class OnObjectDestroyed : MonoBehaviour
    {
        public Action<GameObject> OnDestroyCallback; 
        
        private void OnDestroy()
        {
            OnDestroyCallback?.Invoke(gameObject);
        }
    }
}