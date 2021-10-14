using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid<TGridObject>
{
    public int Width {
        get;
        private set;
    }
    public int Height {
        get;
        private set;
    }
    private TGridObject[,] gridArray;
    public int Scale {
        get;
        private set;
    }

    public TileGrid(int width, int height, int scale)
    {
        this.Width = width;
        this.Height = height;
        this.Scale = scale;

        gridArray = new TGridObject[width, height];

        if(Scale == 0) {//setting the scale to -1 or 0 will not draw the debug lines
            Scale = 1;
        } else if(Scale < 0) {
            Scale *= -1;
        } else {
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
    }

    public void SetValue(int x, int y, TGridObject value)
    {
        if(x >= 0 && x < Width && y >= 0 && y < Height) // If true then the coordinates are within the grid
        {
            gridArray[x, y] = value;
        }
    }

    public void SetValue(Vector3 worldPostion, TGridObject value)
    {
        int x, y;
        GetXY(worldPostion / Scale, out x, out y);
        SetValue(x, y, value);
    }

    public TGridObject GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < Width && y < Height) {
            return gridArray[x,y];
        } else {
            return default(TGridObject);
        }
    }

    public TGridObject GetValue(Vector3 worldPostion) 
    {
        int x, y;
        GetXY(worldPostion / Scale, out x, out y);
        return GetValue(x, y);
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x);
        y = Mathf.FloorToInt(worldPosition.y);
    }
}
