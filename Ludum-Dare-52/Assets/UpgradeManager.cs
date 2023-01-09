using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField]
    private TileBase field;

    [SerializeField]
    private TileBase fieldNotSet;

    [SerializeField]
    private Tilemap fieldMap;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UpgradeField();
        }
    }

    private void UpgradeField()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3Int tilePos = fieldMap.WorldToCell(pos);
        TileBase tile = fieldMap.GetTile(tilePos);
        if (tile == fieldNotSet)
        {
            Debug.Log("IS TILE!");
            if (fieldMap.GetTile(tilePos + Vector3Int.up) == field)
            {
                Blob2();
            }
            else
            {
                Blob();
            }
        }

        void Blob()
        {
            BoundsInt area = new BoundsInt(new Vector3Int(tilePos.x - 1, tilePos.y -1), new Vector3Int(3,3,1));
            Debug.Log(area);
            TileBase[] tiles = new TileBase[area.size.x * area.size.y * area.size.z];
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = field;
            }
            fieldMap.SetTilesBlock(area, tiles);
        }
        
        void Blob2()
        {
            BoundsInt area = new BoundsInt(new Vector3Int(tilePos.x - 1, tilePos.y), new Vector3Int(3,1,1));
            Debug.Log(area);
            TileBase[] tiles = new TileBase[area.size.x * area.size.y * area.size.z];
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = field;
            }
            fieldMap.SetTilesBlock(area, tiles);
        }

        // fieldMap.BoxFill(leftCorner, field, 0,0,rightCorner.x, rightCorner.y);
    }
}