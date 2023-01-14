using System.Linq;
using DG.Tweening;
using LudumDare52.DayNightCycle;
using LudumDare52.Progress;
using LudumDare52.Systems;
using LudumDare52.Systems.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayEndDisplay : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private TextMeshProUGUI txtDay;

    [SerializeField]
    private GameObject uiUnlockContainer;

    [SerializeField]
    private GameObject uiUnlockItemPrefab;

    [SerializeField]
    private Sprite unlockFieldImage;

    private void Start()
    {
        GameManager.Instance.OnStateUpdate += OnStateUpdate;
        HideUi();
    }

    private void OnStateUpdate(GameState state)
    {
        if (state == GameState.DayEnd)
        {
            ShowUi();
        }else if (state == GameState.Running)
        {
            HideUi();
        }
    }

    private void HideUi()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    private void ShowUi()
    {
        AudioSystem.Instance.PlaySound(ResourceSystem.Instance.progess);

        
        int nextDay = GameManager.Instance.Day + 1;

        foreach (Transform child in uiUnlockContainer.transform)
        {
            Destroy(child.gameObject);
        }

        ProgressStep? stepWrapper = Progressmanager.Instance.GetStep(nextDay);

        if (stepWrapper.HasValue)
        {
            ProgressStep setp = stepWrapper.Value;
            if (setp.crop != null)
            {
                SpawnProgressDisplay(setp.crop.displaySpriteUi);
            }

            if (setp.upgradeField)
            {
                SpawnProgressDisplay(unlockFieldImage);
            }
        }

        txtDay.text = (nextDay-1).ToString();

        canvasGroup.DOFade(endValue: 1, duration: 0.5f);
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    private void SpawnProgressDisplay(Sprite icon)
    {
        GameObject setpDisplay = Instantiate(original: uiUnlockItemPrefab, parent: uiUnlockContainer.transform, worldPositionStays: false);
        setpDisplay.GetComponentsInChildren<Image>().Last().sprite = icon;
    }

    public void OnStartNewDayClicked()
    {
        GameManager.Instance.StartDayNext();
    }
}