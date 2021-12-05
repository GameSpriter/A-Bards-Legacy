using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesetController
{
    private TileGrid<GameObject> tileGrid;


    public void setupTileset(TileGrid<GameObject> tileGrid) {
        this.tileGrid = tileGrid;

        for(int x = 0; x < tileGrid.Width; x++) {
            for(int y = 0; y < tileGrid.Height; y++) {
                //Itterate through the grid
                if(tileGrid.GetValue(x, y) != null) { //Check to make sure there is a tile in the grid at the (x, y) location
                    if(tileTypeisFloor(x, y)) { //Floor
                        //TODO: Modify floor based on what type it is
                    } else if(tileTypeisWall(x, y)) {//Wall

                    }
                }
            }
        }
    }

    private bool isNorthWall(int x, int y) {
        if(tileTypeisWall(x + 1, y) && tileTypeisWall(x - 1, y) && tileTypeisWall(x, y + 1) && tileTypeisFloor(x, y - 1)) {
            return true;
        }

        return false;
    }

    private bool tileTypeisFloor(int x, int y) {
        if(tileGrid.GetValue(x, y).GetComponent<TileGameObject>().tileType == DUtils.DungeonUtils.TileType.Floor) {
            return true;
        } else {
            return false;
        }
    }

    private bool tileTypeisWall(int x, int y) {
        if(tileGrid.GetValue(x, y).GetComponent<TileGameObject>().tileType == DUtils.DungeonUtils.TileType.Wall) {
            return true;
        } else {
            return false;
        }
    }
}
