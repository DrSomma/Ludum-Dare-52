using System;
using UnityEngine;

namespace LudumDare52
{
    public class Interactable : MonoBehaviour
    {
        public Action OnPlayerIsClose;
        public Action OnPlayerLeft;
        public Action OnLeftClick;

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
    }
}