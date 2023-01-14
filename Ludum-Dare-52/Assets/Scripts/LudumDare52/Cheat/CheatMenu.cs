using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DG.Tweening;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.DayNightCycle;
using LudumDare52.Storage.Money;
using LudumDare52.Systems.Manager;
using TMPro;
using UnityEngine;

namespace LudumDare52.Cheat
{
    internal interface ICheat
    {
        public KeyCode Hotkey { get; }
        public string DisplayName { get; }
        public bool Activ { get; }

        public void Trigger();
    }

    internal record ToggleCheat : ICheat
    {
        public ToggleCheat(
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

        public void Trigger()
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

    internal record Cheat(string DisplayName, KeyCode Hotkey, Action OnTrigger) : ICheat
    {
        public Action OnTrigger { get; set; } = OnTrigger;
        public KeyCode Hotkey { get; } = Hotkey;
        public string DisplayName { get; } = DisplayName;
        public bool Activ { get; } = false;

        public void Trigger()
        {
            OnTrigger?.Invoke();
        }
    }


    public class CheatMenu : MonoBehaviour
    {
        private static readonly KeyCode[] KeyCodes = {KeyCode.F2, KeyCode.F3,KeyCode.F4,KeyCode.F5,KeyCode.F6};

        [SerializeField]
        private TextMeshProUGUI txtCheats;

        [SerializeField]
        private TextMeshProUGUI txtDebug;
        
        [SerializeField]
        private CanvasGroup uiContainer;

        [SerializeField]
        private GameObject player;

        [SerializeField]
        private Item egg;

        private readonly List<ICheat> _cheats = new();

        private bool _isActiv;

        private void Start()
        {
            _cheats.Add(
                new ToggleCheat(
                    displayName: "growtime",
                    activ: false,
                    hotkey: KeyCodes[0],
                    onActiv: () => { TimeManager.Instance.TimeGrowMultiplier = 10; },
                    onInactive: () => { TimeManager.Instance.TimeGrowMultiplier = 1; }));
            _cheats.Add(
                new ToggleCheat(
                    displayName: "daytime",
                    activ: false,
                    hotkey: KeyCodes[1],
                    onActiv: () => { TimeManager.Instance.TimeDayMultiplier = 15; },
                    onInactive: () => { TimeManager.Instance.TimeDayMultiplier = 1; }));
            _cheats.Add(new Cheat(DisplayName: "spawn egg", Hotkey: KeyCodes[2], OnTrigger: () => { WorldEntiySpawner.Instance.Spawn(item: egg, position: player.transform.position); }));
            _cheats.Add(new Cheat(DisplayName: "add 50  <sprite=0>", Hotkey: KeyCodes[3], OnTrigger: () => { MoneyManager.Instance.AddMoney(50);}));
            SetUiActiv();
            UpdateUi();
        }

        private float _avgFrameRate;
        
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

            _avgFrameRate = Time.frameCount / Time.time;

            
            foreach (ICheat cheat in _cheats.Where(cheat => Input.GetKeyDown(cheat.Hotkey)))
            {
                cheat.Trigger();
                UpdateUi();
            }
            
            UpdateDebugUi();
        }

        private void UpdateDebugUi()
        {
            StringBuilder builder = new();
            builder.Append("FPS: ");
            builder.Append(_avgFrameRate);
            builder.Append("\n");
            builder.Append("Time (24h): ");
            builder.Append($"Value: {TimeManager.Instance.DayTimeInPercent:P2}.");
            builder.Append("\n");
            builder.Append("GameState:");
            builder.Append(GameManager.Instance.State);
            builder.Append("\n");
            txtDebug.text = builder.ToString();
        }

        private void SetUiActiv()
        {
            uiContainer.DOFade(endValue: _isActiv ? 1 : 0, duration: 0.2f);
        }

        private void UpdateUi()
        {
            StringBuilder builder = new();
            builder.Append("F1 hide/show ");
            foreach (ICheat cheat in _cheats)
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