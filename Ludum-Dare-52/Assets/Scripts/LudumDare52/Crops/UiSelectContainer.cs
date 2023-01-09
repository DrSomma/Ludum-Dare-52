using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Progress;
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
            Progressmanager.Instance.OnUpdate += OnUpdate;
        }

        private void OnUpdate(ProgressStep progressStep)
        {
            if (progressStep.crop == null && _crop == progressStep.crop)
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