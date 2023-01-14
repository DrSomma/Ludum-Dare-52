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

        [SerializeField]
        private GameObject fieldSelector;

        private readonly List<UiSelectContainer> _selectContainers = new();

        private CanvasGroup _canvasGroup;
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
                container.Init(index: index, crop: crop);
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

                if (!_selectContainers[index].IsActiv)
                {
                    return;
                }

                Clean();
                _selectContainers[index].SetSelected(true);
                _selectedCrop = ResourceSystem.Instance.CropsList[index];
            }

            void Clean()
            {
                foreach (var container in _selectContainers)
                {
                    container.SetSelected(false);
                }
            }
        }

        private void OnDestroy()
        {
            if (PlayerInteraction.Instance != null)
            {
                PlayerInteraction.Instance.OnNearestChange -= OnNearestChange;
            }
        }

        private void OnNearestChange(Interactable interactable)
        {
            if (interactable != null && interactable.gameObject.CompareTag("CropSpace"))
            {
                fieldSelector.SetActive(true);
                fieldSelector.transform.position = interactable.transform.position;
                ShowUi();
            }
            else
            {
                fieldSelector.SetActive(false);
                fieldSelector.transform.position = Vector3.zero;
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