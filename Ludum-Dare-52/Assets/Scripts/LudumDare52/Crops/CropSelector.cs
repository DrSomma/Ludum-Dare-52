using System.Collections.Generic;
using System.Linq;
using Amazeit.Utilities.Singleton;
using DG.Tweening;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Player;
using LudumDare52.Systems;
using UnityEngine;

namespace LudumDare52.Crops
{
    public class CropSelector : Singleton<CropSelector>
    {
        [SerializeField]
        private Transform uiContainer;

        [SerializeField]
        private GameObject holderPrefab;

        private CanvasGroup _canvasGroup;

        private readonly List<UiSelectContainer> _selectContainers = new();
        private Crop _selectedCrop;

        private void Start()
        {
            PlayerInteraction.Instance.OnNearestChange += OnNearestChange;
            _canvasGroup = uiContainer.GetComponent<CanvasGroup>();
            HideUi();

            for (int index = 0; index < ResourceSystem.Instance.CropsList.Length; index++)
            {
                Crop crop = ResourceSystem.Instance.CropsList[index];
                GameObject holder = Instantiate(original: holderPrefab, parent: uiContainer, worldPositionStays: false);
                UiSelectContainer container = holder.GetComponent<UiSelectContainer>();
                container.Init(index: index, sprite: crop.displaySpriteUi);
                _selectContainers.Add(container);
            }

            _selectContainers.First().SetSelected(true);
            _selectedCrop = ResourceSystem.Instance.CropsList.First();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectCrop(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectCrop(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SelectCrop(2);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SelectCrop(3);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SelectCrop(4);
            }

            void SelectCrop(int index)
            {
                if (index >= _selectContainers.Count)
                {
                    return;
                }

                Clean();
                _selectContainers[index].SetSelected(true);
                _selectedCrop = ResourceSystem.Instance.CropsList[index];
            }

            void Clean()
            {
                _selectContainers[1].SetSelected(false);
                _selectContainers[0].SetSelected(false);
                _selectContainers[2].SetSelected(false);
                _selectContainers[3].SetSelected(false);
                _selectContainers[4].SetSelected(false);
            }
        }

        private void OnDestroy()
        {
            PlayerInteraction.Instance.OnNearestChange -= OnNearestChange;
        }

        private void OnNearestChange(Interactable interactable)
        {
            if (interactable != null && interactable.gameObject.CompareTag("CropSpace"))
            {
                ShowUi();
            }
            else
            {
                HideUi();
            }
        }

        private void HideUi()
        {
            _canvasGroup.DOFade(endValue: 0, duration: 0.3f);
        }

        private void ShowUi()
        {
            _canvasGroup.DOFade(endValue: 1, duration: 0.3f);
        }

        public Crop GetCrop()
        {
            return _selectedCrop;
        }
    }
}