using DG.Tweening;
using LudumDare52.DayNightCycle;
using TMPro;
using UnityEngine;

namespace LudumDare52.Cheat
{
    public class CheatMenu : MonoBehaviour
    {
        [SerializeField]
        private bool growSpeed;

        [SerializeField]
        private TextMeshProUGUI txtCheats;
        [SerializeField]
        private CanvasGroup uiContainer;

        private bool _isActiv;

        private void Start()
        {
            SetUiActiv();
            UpdateUi();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                _isActiv = !_isActiv;
                SetUiActiv();
            }
            if (!_isActiv)
            {
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.F2))
            {
                growSpeed = !growSpeed;
                TimeManager.Instance.TimeGrowMultiplier = growSpeed ? 10f : 1f;
                UpdateUi();
            }
        }

        private void SetUiActiv()
        {
            if (_isActiv)
            {
                uiContainer.DOFade(1, 0.2f);
            }
            else
            {
                uiContainer.DOFade(0, 0.2f);
            }
        }

        private void UpdateUi()
        {
            txtCheats.text = "F1 hide/show ";
            txtCheats.text += (growSpeed ? "<color=\"green\">" : "<color=\"white\">") + "F2 growtime";
        }
    }
}