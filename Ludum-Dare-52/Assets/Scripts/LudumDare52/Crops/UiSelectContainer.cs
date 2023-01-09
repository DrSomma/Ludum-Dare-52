using System;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Progress;
using LudumDare52.Systems.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare52.Crops
{
    public class UiSelectContainer : MonoBehaviour
    {
        [SerializeField]
        private Image displayIcon;
        [SerializeField]
        private TextMeshProUGUI hotkey;
        [SerializeField]
        private GameObject selectedImage;

        private Crop _crop;
        
        public bool IsActiv { get; private set; }

        private void Start()
        {
            GameManager.Instance.OnStateUpdate += OnDayEnd;
        }
        
        private void OnDayEnd(GameState state)
        {
            if(state != GameState.DayEnd || _crop == null)
                return;

            SetCropIsActiv(Progressmanager.Instance.IsCropActiv(_crop));
        }

        public void SetSelected(bool selected)
        {
            selectedImage.SetActive(selected);
        }

        public void Init(int index, Crop crop)
        {
            hotkey.text = (index+1).ToString();
            displayIcon.sprite = crop.displaySpriteUi;
            _crop = crop;
            SetCropIsActiv(Progressmanager.Instance.IsCropActiv(_crop));
            SetSelected(false);
        }

        private void SetCropIsActiv(bool isActive)
        {
            IsActiv = isActive;
            displayIcon.color = isActive ? Color.white : Color.black;
        }
    }
}