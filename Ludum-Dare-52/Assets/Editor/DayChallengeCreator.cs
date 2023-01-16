using LudumDare52.Crops.ScriptableObject;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class DayChallengeCreator : EditorWindow
    {
        
        
        private void OnGUI()
        {
            CropsList = Resources.LoadAll<Crop>("Items/Crops");


            for (int index = 0; index < CropsList.Length; index++)
            {
                Crop crop = CropsList[index];
                EditorGUILayout.SelectableLabel("20x ");
                DrawTexturePreview(new Rect(30, (10 * index) + 10, 20, 20), crop.displaySpriteUi);
            }


            EditorGUILayout.SelectableLabel("End");
        }

        [MenuItem("Window/DayChallengeCreator")]
        public static void ShowWidow()
        {
            Debug.Log("Show!");
            CropsList = Resources.LoadAll<Crop>("Items/Crops");
            ItemList = Resources.LoadAll<Item>("Items");
            GetWindow<DayChallengeCreator>("DayChallengeCreator");
        }
        
        private void DrawTexturePreview(Rect position, Sprite sprite)
        {
            Vector2 fullSize = new Vector2(sprite.texture.width, sprite.texture.height);
            Vector2 size = new Vector2(sprite.textureRect.width, sprite.textureRect.height);
 
            Rect coords = sprite.textureRect;
            coords.x /= fullSize.x;
            coords.width /= fullSize.x;
            coords.y /= fullSize.y;
            coords.height /= fullSize.y;
 
            Vector2 ratio;
            ratio.x = position.width / size.x;
            ratio.y = position.height / size.y;
            float minRatio = Mathf.Min(ratio.x, ratio.y);
 
            Vector2 center = position.center;
            position.width = size.x * minRatio;
            position.height = size.y * minRatio;
            position.center = center;
 
            GUI.DrawTextureWithTexCoords(position, sprite.texture, coords);
        }

        public static Crop[] CropsList { get; set; }

        public static Item[] ItemList { get; set; }
    }
}