using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid
{
    private int width;
    private int height;
    private int[,] gridArray;
    private GameObject[,] gameObjectArray;
    private int scale;

    public enum TileType { Wall, Floor }

    public TileGrid(int width, int height, int scale)
    {
        this.width = width;
        this.height = height;
        this.scale = scale;

        gridArray = new int[width, height];

        for(int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                Debug.DrawLine(new Vector2(x, y) * scale, new Vector2(x, y + 1) * scale, Color.white, 100.0f);
                Debug.DrawLine(new Vector2(x, y) * scale, new Vector2(x + 1, y) * scale, Color.white, 100.0f);

                
            }
        }
        Debug.DrawLine(new Vector2(0, height) * scale, new Vector2(width, height) * scale, Color.white, 100.0f);
        Debug.DrawLine(new Vector2(width, 0) * scale, new Vector2(width, height) * scale, Color.white, 100.0f);
    }

    public void SetTile(int x, int y, TileType tileType)
    {
        if(x >= 0 && x < width && y >= 0 && y < height) // If true then the coordinates are within the grid
        {
            gridArray[x, y] = (int) tileType;
        }
    }

    public void SetTile(Vector3 worldPostion, TileType tileType)
    {
        int x, y;
        GetXY(worldPostion / scale, out x, out y);
        SetTile(x, y, tileType);
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x);
        y = Mathf.FloorToInt(worldPosition.y);
    }
}
