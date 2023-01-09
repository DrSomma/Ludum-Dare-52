using System.ComponentModel;
using UnityEngine;

namespace LudumDare52.Crops.ScriptableObject
{
    [CreateAssetMenu(fileName = "New Crop")]
    public class Crop : Item
    {
        [Description("in seconds")]
        [Tooltip("in seconds")]
        public float growtimeInSeconds;

        public Sprite[] stages;
        public int price;
        public Sprite displaySpriteUi;
    }
    
    [CreateAssetMenu(fileName = "New Item")]
    public class Item : UnityEngine.ScriptableObject
    {
        public Sprite displaySpriteUi;
        public int price;
    }
}