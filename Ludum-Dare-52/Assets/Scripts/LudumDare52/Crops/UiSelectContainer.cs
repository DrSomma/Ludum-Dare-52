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

        public void SetSelected(bool selected)
        {
            selectedImage.SetActive(selected);
        }

        public void Init(int index, Sprite sprite)
        {
            hotkey.text = (index+1).ToString();
            displayIcon.sprite = sprite;
        }
    }
}