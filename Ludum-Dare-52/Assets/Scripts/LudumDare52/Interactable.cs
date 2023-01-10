using System;
using UnityEngine;

namespace LudumDare52
{
    public class Interactable : MonoBehaviour
    {
        public Action OnPlayerEnter;
        public Action OnPlayerLeft;
        public Action OnPlayerExit;
        public Action OnRightClick;
        public Action<bool> OnCanInteractChanged;
        private bool _canInteract;

        public bool CanInteract => _canInteract;

        public void SetCanInteract(bool activ)
        {
            _canInteract = activ;
            OnCanInteractChanged?.Invoke(activ);
        }

        public void TriggerPlayerIsClose()
        {
            OnPlayerEnter?.Invoke();
        }
        
        public void TriggerPlayerLeft()
        {
            OnPlayerLeft?.Invoke();
        }

        public void TriggerLeftClick()
        {
            OnPlayerExit?.Invoke();
        }
        
        public void TriggerRightClick()
        {
            OnRightClick?.Invoke();
        }
    }
}