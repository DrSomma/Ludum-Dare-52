using System.Collections;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Entitys.Interactable;
using LudumDare52.Storage;
using UnityEngine;

namespace LudumDare52.ItemTransformer
{
    public class ItemTransformer : MonoBehaviour
    {
        [SerializeField]
        private Interactable interactable;

        [SerializeField]
        protected Item input;

        [SerializeField]
        protected Item output;

        [SerializeField]
        protected float timeInSeconds;

        [SerializeField]
        protected SimpleStorage containerInput;

        protected virtual void Start()
        {
            interactable.OnLeftClick += OnLeftClick;
        }
        
        private void OnLeftClick()
        {
            if (!containerInput.HasSpace || !MainStorage.Instance.Container.HasItem(input))
            {
                return;
            }

            MainStorage.Instance.Container.TryRemoveFromStorage(input);
            containerInput.Storage.AddToStorage(input);
            
            StartCoroutine(Produce());
        }

        protected virtual IEnumerator Produce()
        {
            yield return new WaitForSeconds(timeInSeconds);

            MainStorage.Instance.Container.AddToStorage(output);
        }
    }
}