using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DG.Tweening;
using LudumDare52.DayNightCycle;
using TMPro;
using UnityEngine;

namespace LudumDare52.Cheat
{
    public record Cheat
    {
        public Cheat(
            string displayName,
            bool activ,
            KeyCode hotkey,
            Action onActiv,
            Action onInactive)
        {
            OnActiv = onActiv;
            OnInactive = onInactive;
            DisplayName = displayName;
            Activ = activ;
            Hotkey = hotkey;
            OnActiv = onActiv;
            OnInactive = onInactive;
        }

        public Action OnActiv { get; }
        public Action OnInactive { get; }
        public string DisplayName { get; }
        public bool Activ { get; private set; }
        public KeyCode Hotkey { get; }

        public void ChangeActiv()
        {
            Activ = !Activ;
            if (Activ)
            {
                OnActiv?.Invoke();
            }
            else
            {
                OnInactive?.Invoke();
            }
        }
    }

    public class CheatMenu : MonoBehaviour
    {
        private static readonly KeyCode[] KeyCodes = {KeyCode.F2, KeyCode.F3};

        [SerializeField]
        private TextMeshProUGUI txtCheats;

        [SerializeField]
        private CanvasGroup uiContainer;

        private readonly List<Cheat> _cheats = new();

        private bool _isActiv;

        private void Start()
        {
            _cheats.Add(
                new Cheat(
                    displayName: "growtime",
                    activ: false,
                    hotkey: KeyCodes[0],
                    onActiv: () => { TimeManager.Instance.TimeGrowMultiplier = 10; },
                    onInactive: () => { TimeManager.Instance.TimeGrowMultiplier = 1; }));
            _cheats.Add(
                new Cheat(
                    displayName: "daytime",
                    activ: false,
                    hotkey: KeyCodes[1],
                    onActiv: () => { TimeManager.Instance.TimeDayMultiplier = 15; },
                    onInactive: () => { TimeManager.Instance.TimeDayMultiplier = 1; }));
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

            foreach (Cheat cheat in _cheats.Where(cheat => Input.GetKeyDown(cheat.Hotkey)))
            {
                cheat.ChangeActiv();
                UpdateUi();
            }
        }

        private void SetUiActiv()
        {
            uiContainer.DOFade(endValue: _isActiv ? 1 : 0, duration: 0.2f);
        }

        private void UpdateUi()
        {
            StringBuilder builder = new();
            builder.Append("F1 hide/show ");
            foreach (Cheat cheat in _cheats)
            {
                builder.Append(cheat.Activ ? "<color=\"green\">" : "<color=\"white\">");
                builder.Append(" ");
                builder.Append(cheat.Hotkey);
                builder.Append(" ");
                builder.Append(cheat.DisplayName);
            }

            txtCheats.text = builder.ToString();
        }
    }
}