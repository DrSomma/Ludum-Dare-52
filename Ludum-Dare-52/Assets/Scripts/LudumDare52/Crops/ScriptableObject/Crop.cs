using System.ComponentModel;
using UnityEngine;

namespace LudumDare52.Crops.ScriptableObject
{
    [CreateAssetMenu(fileName = "New Crop")]
    public class Crop : UnityEngine.ScriptableObject
    {
        [Description("in seconds")]
        [Tooltip("in seconds")]
        public float growtimeInSeconds;
        public Sprite[] stages;
        public int price;
        public Sprite displaySpriteUi;
    }
}