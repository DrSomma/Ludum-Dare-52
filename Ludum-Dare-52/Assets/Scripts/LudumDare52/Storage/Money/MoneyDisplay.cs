using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace LudumDare52.Storage.Money
{
    public class MoneyDisplay : MonoBehaviour
    {
        [SerializeField]
        private bool doAnimation = true;

        private TextMeshProUGUI _txtMoney;
        private Vector3 _scale;

        private void Start()
        {
            _txtMoney = GetComponent<TextMeshProUGUI>();
            MoneyManager.Instance.OnUpdateMoney += OnUpdateMoney;
            _scale = transform.localScale;
        }

        private void OnUpdateMoney(int change, int newMoneyValue, int target)
        {
            _txtMoney.text = $"{newMoneyValue}/{target} <sprite=0>";
            if(!doAnimation || newMoneyValue == 0)
                return;
            _txtMoney.transform.DOKill();
            _txtMoney.transform.DOPunchScale(punch: Vector3.one * 0.2f, duration: 0.3f, vibrato: 1).OnKill(
                () =>
                {
                    _txtMoney.transform.localScale = _scale;
                });
        }
    }
}