using System;
using System.Collections.Generic;
using UnityEngine;

public class TilesetController : MonoBehaviour
{
    private TileGrid<GameObject> tileGrid;

    #region Sprites
    public Sprite BrickWall_E;
    public Sprite BrickWall_N;
    public Sprite BrickWall_NE_NW_SE;
    public Sprite BrickWall_NE_NW_SW;
    public Sprite BrickWall_NE_NW;
    public Sprite BrickWall_NE_SE;
    public Sprite BrickWall_NE_SW_SE;
    public Sprite BrickWall_NE_SW;
    public Sprite BrickWall_NE_Inner;
    public Sprite BrickWall_NE_Outer;
    public Sprite BrickWall_NW_SE;
    public Sprite BrickWall_NW_SW_SE;
    public Sprite BrickWall_NW_SW;
    public Sprite BrickWall_NW_Inner;
    public Sprite BrickWall_NW_Outer;
    public Sprite BrickWall_Parallel_E_W;
    public Sprite BrickWall_Parallel_N_S;
    public Sprite BrickWall_S;
    public Sprite BrickWall_SE_Inner;
    public Sprite BrickWall_Side_Pillar_E;
    public Sprite BrickWall_Side_Pillar_W;
    public Sprite BrickWall_Solo;
    public Sprite BrickWall_SW_SE;
    public Sprite BrickWall_SW_Inner;
    public Sprite BrickWall_U_East;
    public Sprite BrickWall_U_North;
    public Sprite BrickWall_U_South;
    public Sprite BrickWall_U_West;
    public Sprite BrickWall_Void;
    public Sprite BrickWall_W_SE;
    public Sprite BrickWall_W;
    #endregion

    public void setupTileset(TileGrid<GameObject> tileGrid) {
        this.tileGrid = tileGrid;

        for(int x = 0; x < tileGrid.Width; x++) {
            for(int y = 0; y < tileGrid.Height; y++) {
                //Itterate through the grid
                if(tileGrid.GetValue(x, y) != null) { //Check to make sure there is a tile in the grid at the (x, y) location
                    if(tileTypeisFloor(x, y)) { //Floor
                        //TODO: Modify floor based on what type it is
                    } else if(tileTypeisWall(x, y)) {//Wall
                        setWallSprite(x, y);
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
        if(getTileType(x, y) == DUtils.DungeonUtils.TileType.Floor) {
            return true;
        } else {
            return false;
        }
    }

    private bool tileTypeisWall(int x, int y) {
        if(tileGrid.GetValue(x, y) == null) {
            return true;
        } else if(getTileType(x, y) == DUtils.DungeonUtils.TileType.Wall) {
            return true;
        } else {
            return false;
        }
    }

    private DUtils.DungeonUtils.TileType getTileType(int x, int y) {
        return tileGrid.GetValue(x, y).GetComponent<TileGameObject>().tileType;
    }

    private int getTileCode(int x, int y) {
        int tileCode = 0;

        /*  Tile Code Key
             2 |  4 |  8
             1 |    | 16
            128| 64 | 32 
        */

        if(tileTypeisWall(x - 1, y)) {
            tileCode += 128;
        }
        if(tileTypeisWall(x - 1, y + 1)) {
            tileCode += 64;
        }
        if(tileTypeisWall(x, y + 1)) {
            tileCode += 32;
        }
        if(tileTypeisWall(x + 1, y + 1)) {
            tileCode += 16;
        }
        if(tileTypeisWall(x + 1, y)) {
            tileCode += 8;
        }
        if(tileTypeisWall(x + 1, y - 1)) {
            tileCode += 4;
        }
        if(tileTypeisWall(x, y - 1)) {
            tileCode += 2;
        }
        if(tileTypeisWall(x - 1, y - 1)) {
            tileCode += 1;
        }

        return tileCode;
    }

    private void setSprite(int x, int y, Sprite sprite) {
        tileGrid.GetValue(x, y).GetComponent<SpriteRenderer>().sprite = sprite;
        tileGrid.GetValue(x, y).name += "" + getTileCode(x, y); //Debug
        if(x == 18) {
            Debug.Log("(" + x + ", " + y + "): " + Convert.ToString(getTileCode(x, y), 2));
        }
    }

    private void setWallSprite(int x, int y) {
        switch(getTileCode(x, y)) {
            //I do appologise, these are not in binary order or any specific order other than the same order that is in the Sprites folder, which I think is alphabetical
                // 0x11111x
            case 0b00111110:
            case 0b00111111:
            case 0b01111110:
            case 0b01111111:
                setSprite(x, y, BrickWall_E);
                break;
                // 11111x0x
            case 0b11111101:
            case 0b11111100:
            case 0b11111001:
            case 0b11111000:
                setSprite(x, y, BrickWall_N);
                break;
                // 0x11101x
            case 0b01111010:
            case 0b01111011:
            case 0b00111010:
            case 0b00111011:
                setSprite(x, y, BrickWall_NE_NW_SE);
                break;
                // 111x0x10
            case 0b11110010:
            case 0b11110110:
            case 0b11100010:
            case 0b11100110:
                setSprite(x, y, BrickWall_NE_NW_SW);
                break;
                // 11111010
            case 0b11111010:
                setSprite(x, y, BrickWall_NE_NW);
                break;
                // 10101110
            case 0b10101110:
                setSprite(x, y, BrickWall_NE_SW_SE);
                break;
                // 11101110
            case 0b11101110:
                setSprite(x, y, BrickWall_NE_SW);
                break;
                // 11111110
            case 0b11111110:
                setSprite(x, y, BrickWall_NE_Inner);
                break;
                // 1x0x0x11
            case 0b11010111:
            case 0b11010011:
            case 0b11000111:
            case 0b11000011:
            case 0b10010111:
            case 0b10010011:
            case 0b10000111:
            case 0b10000011:
                setSprite(x, y, BrickWall_NE_Outer);
                break;
                // 1011101x
            case 0b10110011:
            case 0b10110010:
                setSprite(x, y, BrickWall_NW_SE);
                break;
                // 10101011
            case 0b10100011:
                setSprite(x, y, BrickWall_NW_SW_SE);
                break;
                // 11101011
            case 0b11101011:
                setSprite(x, y, BrickWall_NW_SW);
                break;
                // 11111011
            case 0b11111011:
                setSprite(x, y, BrickWall_NW_Inner);
                break;
                // 000x111x
            case 0b00011111:
            case 0b00011110:
            case 0b00001111:
            case 0b00001110:
                setSprite(x, y, BrickWall_NW_Outer);
                break;
                // 0x1x0x1x
            case 0b01110111:
            case 0b01110110:
            case 0b01110011:
            case 0b01110010:
            case 0b01100111:
            case 0b01100110:
            case 0b01100011:
            case 0b01100010:
            case 0b00110111:
            case 0b00110110:
            case 0b00110011:
            case 0b00110010:
            case 0b00100111:
            case 0b00100110:
            case 0b00100011:
            case 0b00100010:
                setSprite(x, y, BrickWall_Parallel_E_W);
                break;
                // 1x0x1x0x
            case 0b11011101:
            case 0b11011100:
            case 0b11011001:
            case 0b11011000:
            case 0b11001101:
            case 0b11001100:
            case 0b11001001:
            case 0b11001000:
            case 0b10011101:
            case 0b10011100:
            case 0b10011001:
            case 0b10011000:
            case 0b10001101:
            case 0b10001100:
            case 0b10001001:
            case 0b10001000:
                setSprite(x, y, BrickWall_Parallel_N_S);
                break;
                // 1x0x1111
            case 0b11011111:
            case 0b11001111:
            case 0b10011111:
            case 0b10001111:
                setSprite(x, y, BrickWall_S);
                break;
                // 10111x1x
            case 0b10111111:
            case 0b10111110:
            case 0b10111011:
            case 0b10111010:
                setSprite(x, y, BrickWall_SE_Inner);
                break;
                // 0x111x0x
            case 0b01111101:
            case 0b01111100:
            case 0b01111001:
            case 0b01111000:
            case 0b00111101:
            case 0b00111100:
            case 0b00111001:
            case 0b00111000:
                setSprite(x, y, BrickWall_Side_Pillar_E);
                break;
                // 111x0x0x
            case 0b11110101:
            case 0b11110100:
            case 0b11110001:
            case 0b11110000:
            case 0b11100101:
            case 0b11100100:
            case 0b11100001:
            case 0b11100000:
                setSprite(x, y, BrickWall_Side_Pillar_W);
                break;
                // 0x0x0x0x
            case 0b01010101:
            case 0b01010100:
            case 0b01010001:
            case 0b01010000:
            case 0b01000101:
            case 0b01000100:
            case 0b01000001:
            case 0b01000000:
            case 0b00010101:
            case 0b00010100:
            case 0b00010001:
            case 0b00010000:
            case 0b00000101:
            case 0b00000100:
            case 0b00000001:
            case 0b00000000:
                setSprite(x, y, BrickWall_Solo);
                break;
                // 10101111
            case 0b10101111:
                setSprite(x, y, BrickWall_SW_SE);
                break;
                // 11101111
            case 0b11101111:
                setSprite(x, y, BrickWall_SW_Inner);
                break;
                // 1x0x0x0x
            case 0b11010101:
            case 0b11010100:
            case 0b11010001:
            case 0b11010000:
            case 0b11000101:
            case 0b11000100:
            case 0b11000001:
            case 0b11000000:
            case 0b10010101:
            case 0b10010100:
            case 0b10010001:
            case 0b10010000:
            case 0b10000101:
            case 0b10000100:
            case 0b10000001:
            case 0b10000000:
                setSprite(x, y, BrickWall_U_East);
                break;
                // 0x0x0x1x
            case 0b01010111:
            case 0b01010110:
            case 0b01010011:
            case 0b01010010:
            case 0b01000111:
            case 0b01000110:
            case 0b01000011:
            case 0b01000010:
            case 0b00010111:
            case 0b00010110:
            case 0b00010011:
            case 0b00010010:
            case 0b00000111:
            case 0b00000110:
            case 0b00000011:
            case 0b00000010:
                setSprite(x, y, BrickWall_U_North);
                break;
                // 0x1x0x0x
            case 0b01110101:
            case 0b01110100:
            case 0b01110001:
            case 0b01110000:
            case 0b01100101:
            case 0b01100100:
            case 0b01100001:
            case 0b01100000:
            case 0b00110101:
            case 0b00110100:
            case 0b00110001:
            case 0b00110000:
            case 0b00100101:
            case 0b00100100:
            case 0b00100001:
            case 0b00100000:
                setSprite(x, y, BrickWall_U_South);
                break;
                // 0x0x1x0x
            case 0b01011101:
            case 0b01011100:
            case 0b01011001:
            case 0b01011000:
            case 0b01001101:
            case 0b01001100:
            case 0b01001001:
            case 0b01001000:
            case 0b00011101:
            case 0b00011100:
            case 0b00011001:
            case 0b00011000:
            case 0b00001101:
            case 0b00001100:
            case 0b00001001:
            case 0b00001000:
                setSprite(x, y, BrickWall_U_West);
                break;
                // 11111111
            case 0b11111111:
                setSprite(x, y, BrickWall_Void);
                break;
                // 10100111
            case 0b10100111:
                setSprite(x, y, BrickWall_W_SE);
                break;
                // 111x0x11
            case 0b11110111:
            case 0b11110011:
            case 0b11100111:
            case 0b11100011:
                setSprite(x, y, BrickWall_W);
                break;
            default:
                setSprite(x, y, BrickWall_Solo);
                break;
        }
    }

    
}
