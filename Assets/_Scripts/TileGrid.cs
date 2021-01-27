using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public static TileGrid tileGrid
    {
        get;
        private set;
    }

    public enum TileType { Wall, Floor }

    public int gridWidth = 25;
    public int gridHeight = 25;
    public Tile[,] grid;

    public float threshold = 0.2f;
    public float scale = 15f;
    public float seed = 0.0f;

    public GameObject tilePrefab;
    
    void Start()
    {
        //Added reference to this script for ease of use
        tileGrid = this;

        //Setup the grid
        grid = new Tile[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridWidth; y++)
            {
                grid[x, y] = new Tile();

                grid[x, y].TileGameObject = Instantiate(tilePrefab, new Vector2(x, y), Quaternion.identity);
            }
        }

        setNoise();
    }

    void Update()
    {
        setNoise();
    }

    private void setNoise()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridWidth; y++)
            {
                //seed = Time.time;

                float noiseX = (x + seed) / scale;
                float noiseY = (y + seed) / scale;

                if (Mathf.PerlinNoise(noiseX, noiseY) > threshold)
                {
                    grid[x, y].tileType = TileType.Floor;
                }
                else
                {
                    grid[x, y].tileType = TileType.Wall;
                }
                grid[x, y].TileGameObject.GetComponentInChildren<SpriteRenderer>().sprite = GetSprite(grid[x, y].tileType);
            }
        }
    }

    private Sprite GetSprite(TileType type)
    {
        return Resources.LoadAll<Sprite>("Sprite-0001")[(int)type];
    }
}
