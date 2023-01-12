using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare52.ItemTransformer
{
    public class ItemTransformerProgressDisplay : MonoBehaviour
    {
        [SerializeField]
        private Image imageFront;

        [SerializeField]
        private Image imageBack;


        private void Start()
        {
            imageFront.fillAmount = 0;
            imageFront.transform.localScale = Vector3.zero;
            imageBack.transform.localScale = Vector3.zero;
        }

        public void StartAnimation(float timeInSeconds)
        {
            imageFront.fillAmount = 0;

            Sequence sq = DOTween.Sequence();
            sq.Append(imageFront.transform.DOScale(endValue: 1, duration: 0.3f).SetEase(Ease.InBounce));
            sq.Join(imageBack.transform.DOScale(endValue: 1, duration: 0.3f).SetEase(Ease.InBounce));
            sq.Append(imageFront.DOFillAmount(endValue: 1, duration: timeInSeconds - 0.3f).SetEase(Ease.Linear));
            sq.Append(imageFront.transform.DOScale(endValue: 0, duration: 0.3f).SetEase(Ease.InOutBounce));
            sq.Join(imageBack.transform.DOScale(endValue: 0, duration: 0.3f).SetEase(Ease.InOutBounce));
            sq.OnComplete(() => { imageFront.fillAmount = 0; });
        }
    }
}