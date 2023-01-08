using UnityEngine;
using UnityEngine.UI;

namespace LudumDare52.Npc.Order
{
    public class UiOrderItem : MonoBehaviour
    {
        [SerializeField]
        private Image imgItem;

        [SerializeField]
        private Image imgCheckmark;

        public void SetNewOrderItem(Sprite img)
        {
            imgItem.sprite = img;
            imgCheckmark.gameObject.SetActive(false);
        }

        public void SetCheck()
        {
            imgCheckmark.gameObject.SetActive(true);
        }
    }
}