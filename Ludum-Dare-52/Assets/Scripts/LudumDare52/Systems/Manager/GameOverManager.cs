using DG.Tweening;
using TMPro;
using UnityEngine;

namespace LudumDare52.Systems.Manager
{
    public class GameOverManager : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private TextMeshProUGUI textDay;

        private void Start()
        {
            GameManager.Instance.OnStateUpdate += OnStateUpdate;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
            canvasGroup.alpha = 0;
        }

        private void OnStateUpdate(GameState state)
        {
            if (state != GameState.GameOver)
            {
                return;
            }

            ShowUI();
        }

        public void OnReplayClicked()
        {
            GameManager.Instance.RestartDay();
            canvasGroup.DOFade(endValue: 0, duration: 0.3f);
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }

        private void ShowUI()
        {
            textDay.text = GameManager.Instance.Day.ToString();
            canvasGroup.DOFade(endValue: 1, duration: 0.3f);
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }
    }
}