using System;
using UnityEngine;

namespace LudumDare52
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField]
        private bool canInteractOnStart = true;
        
        public Action OnPlayerEnter;
        public Action OnPlayerExit;
        public Action OnLeftClick;
        public Action OnRightClick;
        public Action<bool> OnCanInteractChanged;

        public bool CanInteract { get; private set; }

        private void Start()
        {
            SetCanInteract(canInteractOnStart);
        }

        public void SetCanInteract(bool activ)
        {
            CanInteract = activ;
            OnCanInteractChanged?.Invoke(activ);
        }

        public void TriggerPlayerIsClose()
        {
            OnPlayerEnter?.Invoke();
        }
        
        public void TriggerPlayerLeft()
        {
            OnPlayerExit?.Invoke();
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