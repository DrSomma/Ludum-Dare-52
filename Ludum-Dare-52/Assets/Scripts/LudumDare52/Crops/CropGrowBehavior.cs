using DG.Tweening;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Systems.Manager;
using UnityEngine;

namespace LudumDare52.Crops
{
    public class CropGrowBehavior : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private int _currentStage;

        private float _currentStageGrowTime;

        private bool _isGrowing;
        private float _timeStage;

        public Crop Crop { get; set; }

        public bool IsHarvestable { get; private set; }

        private void Start()
        {
            ClearCrop();

            GameManager.Instance.OnStateUpdate += OnStateUpdate;
        }

        private void Update()
        {
            if (!_isGrowing)
            {
                return;
            }

            _currentStageGrowTime += Time.deltaTime;
            if (!(_currentStageGrowTime >= _timeStage))
            {
                return;
            }

            _currentStageGrowTime = 0;
            _currentStage++;
            spriteRenderer.sprite = Crop.stages[_currentStage];
            if (_currentStage >= Crop.stages.Length - 1)
            {
                _isGrowing = false;
                IsHarvestable = true;
            }
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateUpdate -= OnStateUpdate;
            }
        }

        private void ClearCrop()
        {
            IsHarvestable = false;
            _isGrowing = false;
            Crop = null;
            spriteRenderer.sprite = null;
        }

        private void OnStateUpdate(GameState state)
        {
            if (Crop == null)
            {
                return;
            }

            _isGrowing = state == GameState.Running;
        }

        public void PlantNewCrop(Crop newCrop)
        {
            Crop = newCrop;
            spriteRenderer.sprite = Crop.stages[0];
            _timeStage = Mathf.Max(a: Crop.growtimeInSeconds / (Crop.stages.Length - 1), b: 0);
            _isGrowing = true;
        }


        public void Harvest()
        {
            IsHarvestable = false;
            transform.DOScale(endValue: 0, duration: 0.3f).OnComplete(() => { ClearCrop(); });
        }
    }
}