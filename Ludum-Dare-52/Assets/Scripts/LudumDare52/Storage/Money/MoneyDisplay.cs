using DG.Tweening;
using TMPro;
using UnityEngine;

namespace LudumDare52.Storage.Money
{
    public class MoneyDisplay : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI txtMoney;

        private void Start()
        {
            MoneyManager.Instance.OnUpdateMoney += OnUpdateMoney;
        }

        private void OnUpdateMoney(int change, int newMoneyValue)
        {
            Debug.Log($"you got {change} Coins and now you have {newMoneyValue}");
            txtMoney.text = $"{newMoneyValue} Coins";
            txtMoney.transform.DOKill();
            txtMoney.transform.DOPunchScale(punch: Vector3.one * 0.2f, duration: 0.3f, vibrato: 1);
        }
    }
}