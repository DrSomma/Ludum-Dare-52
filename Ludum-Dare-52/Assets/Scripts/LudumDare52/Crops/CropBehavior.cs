using System.Collections;
using DG.Tweening;
using LudumDare52.Crops.ScriptableObject;
using UnityEngine;
using UnityEngine.Serialization;

namespace LudumDare52.Crops
{
    public class CropBehavior : MonoBehaviour
    {
        [FormerlySerializedAs("renderer")]
        [SerializeField]
        private SpriteRenderer Renderer;

        public Crop Crop { get; set; }

        public bool IsHarvestable { get; private set; }

        private void Start()
        {
            IsHarvestable = false;
            StartCoroutine(Grow());
        }

        private IEnumerator Grow()
        {
            float timeStage = Mathf.Max(a: Crop.growtimeInSeconds / (Crop.stages.Length - 1), b: 0);
            for (int i = 0; i < Crop.stages.Length; i++)
            {
                Renderer.sprite = Crop.stages[i];

                if (i < Crop.stages.Length - 1)
                {
                    yield return new WaitForSeconds(timeStage);
                }
            }

            IsHarvestable = true;
        }

        public void Harvest()
        {
            transform.DOScale(endValue: 0, duration: 0.3f).OnComplete(() => { Destroy(gameObject); });
        }
    }
}