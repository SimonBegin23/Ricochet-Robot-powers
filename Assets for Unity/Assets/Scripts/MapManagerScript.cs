using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManagerScript : MonoBehaviour
{
    [SerializeField] private Tilemap map;

    [SerializeField] private List<TileData> tileDatas;

    private Dictionary<TileBase, TileData> dataFromTiles;

    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach(TileData tileData in tileDatas)
        {
            foreach(Tile tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

    private class Position
    {
        public float x;
        public float y;

        public Position(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            /*Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = map.WorldToCell(mousePosition);

            TileBase clickedTile = map.GetTile(gridPosition);

            Position[] positions = new Position[dataFromTiles[clickedTile].posx.Length];
            for(int i = 0; i < dataFromTiles[clickedTile].posx.Length; i++)
            {
                positions[i] = new Position(dataFromTiles[clickedTile].posx[i], dataFromTiles[clickedTile].posy[i]);
            }
            print("Exits on " + clickedTile + mousePosition + " are ");
            for(int j = 0; j < positions.Length; j++)
            {
                print("(" + dataFromTiles[clickedTile].posx[j] + "," + dataFromTiles[clickedTile].posy[j] + ")   ");
            }*/
        }
    }
}
