using DG.Tweening;
using LudumDare52.Crops.ScriptableObject;
using LudumDare52.Systems.Manager;
using UnityEngine;

namespace LudumDare52.Crops
{
    public class CropBehavior : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private bool _isGrowing;

        private float _currentStageGrowTime;
        private int _currentStage;
        private float _timeStage;

        public Crop Crop { get; set; }

        public bool IsHarvestable { get; private set; }

        private void Start()
        {
            IsHarvestable = false;
            _isGrowing = true;
            spriteRenderer.sprite = Crop.stages[0];
            
            GameManager.Instance.OnStateUpdate += OnStateUpdate;
            _timeStage = Mathf.Max(a: Crop.growtimeInSeconds / (Crop.stages.Length - 1), b: 0);
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
            if (_currentStage == Crop.stages.Length - 1)
            {
                _isGrowing = false;
                IsHarvestable = true;
            }
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnStateUpdate -= OnStateUpdate;
        }

        private void OnStateUpdate(GameState state)
        {
            _isGrowing = state == GameState.Running;
        }


        public void Harvest()
        {
            transform.DOScale(endValue: 0, duration: 0.3f).OnComplete(() => { Destroy(gameObject); });
        }
    }
}