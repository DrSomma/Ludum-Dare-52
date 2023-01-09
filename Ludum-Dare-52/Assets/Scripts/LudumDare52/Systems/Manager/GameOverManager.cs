using System;
using DG.Tweening;
using LudumDare52.DayNightCycle;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LudumDare52.Systems.Manager
{
    public class GameOverManager : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;
        
        [SerializeField]
        private TextMeshProUGUI textDay;
        
        private void Start()
        {
            GameManager.Instance.OnStateUpdate += OnStateUpdate;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0;
        }

        private void OnStateUpdate(GameState state)
        {
            if(state != GameState.GameOver)
                return;

            ShowUI();
        }

        public void OnReplayClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            // SceneManager.LoadScene(0);
        }

        private void ShowUI()
        {
            textDay.text = TimeManager.Instance.Day.ToString();
            _canvasGroup.DOFade(endValue: 1, duration: 0.3f);
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }
    }
}