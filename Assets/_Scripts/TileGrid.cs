using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid<TGridObject>
{
    private int width;
    private int height;
    private TGridObject[,] gridArray;
    public int scale;

    public TileGrid(int width, int height, int scale)
    {
        this.width = width;
        this.height = height;
        this.scale = scale;

        gridArray = new TGridObject[width, height];

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

    public void SetValue(int x, int y, TGridObject value)
    {
        if(x >= 0 && x < width && y >= 0 && y < height) // If true then the coordinates are within the grid
        {
            gridArray[x, y] = value;
        }
    }

    public void SetValue(Vector3 worldPostion, TGridObject value)
    {
        int x, y;
        GetXY(worldPostion / scale, out x, out y);
        SetValue(x, y, value);
    }

    public TGridObject GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height) {
            return gridArray[x,y];
        } else {
            return default(TGridObject);
        }
    }

    public TGridObject GetValue(Vector3 worldPostion) 
    {
        int x, y;
        GetXY(worldPostion / scale, out x, out y);
        return GetValue(x, y);
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x);
        y = Mathf.FloorToInt(worldPosition.y);
    }
}
