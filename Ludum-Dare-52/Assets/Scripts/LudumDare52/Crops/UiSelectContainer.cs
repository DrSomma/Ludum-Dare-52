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
            GameManager.Instance.OnStartDay += OnStartDay;
        }

        private void OnStartDay(int day)
        {
            ProgressStep? progressStep = Progressmanager.Instance.GetStep(day);
            if (!progressStep.HasValue || progressStep.Value.crop != _crop)
            {
                return;
            }

            SetCropIsActiv(true);
        }

        public void SetSelected(bool selected)
        {
            selectedImage.SetActive(selected);
        }

        public void Init(int index, Crop crop)
        {
            hotkey.text = (index + 1).ToString();
            displayIcon.sprite = crop.displaySpriteUi;
            _crop = crop;
            SetCropIsActiv(Progressmanager.Instance.IsActiv(_crop));
            SetSelected(false);
        }

        private void SetCropIsActiv(bool isActiv)
        {
            IsActiv = isActiv;
            displayIcon.color = isActiv ? Color.white : Color.black;
        }
    }
}