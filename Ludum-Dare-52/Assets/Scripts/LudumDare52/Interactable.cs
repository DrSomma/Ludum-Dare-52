using System;
using UnityEngine;

namespace LudumDare52
{
    public class Interactable : MonoBehaviour
    {
        public Action OnPlayerIsClose;
        public Action OnPlayerLeft;
        public Action OnLeftClick;
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
            OnPlayerIsClose?.Invoke();
        }
        
        public void TriggerPlayerLeft()
        {
            OnPlayerLeft?.Invoke();
        }

        public void TriggerLeftClick()
        {
            OnLeftClick?.Invoke();
        }
        
        public void TriggerRightClick()
        {
            OnRightClick?.Invoke();
        }
    }
}