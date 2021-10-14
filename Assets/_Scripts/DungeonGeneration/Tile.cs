using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DUtils;

public class Tile
{
    public DungeonUtils.TileType tileType;

    public override string ToString()
    {
        return "" + (int) tileType;
    }

    public Tile() {
        tileType = (DungeonUtils.TileType) 0;
    }

    public Tile(int tt) {
        tileType = (DungeonUtils.TileType) tt;
    }

}
