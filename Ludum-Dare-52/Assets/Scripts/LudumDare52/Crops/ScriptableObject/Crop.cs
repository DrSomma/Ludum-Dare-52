using System.ComponentModel;
using LudumDare52.Storage;
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
    }
    
    [CreateAssetMenu(fileName = "New Item")]
    public class Item : UnityEngine.ScriptableObject
    {
        public Sprite displaySpriteUi;
        public int price;
        public Sprite DisplaySprite => displaySpriteUi;
    }
}