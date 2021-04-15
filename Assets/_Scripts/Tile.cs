using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public int x
    {
        get;
        private set;
    }
    public int y
    {
        get;
        private set;
    }

    /*
    public TileGrid.TileType tileType
    {
        get;
        set;
    }
    */

    public GameObject TileGameObject;

    public Tile()
    {
        //tileType = TileGrid.TileType.Wall;
    }
}
