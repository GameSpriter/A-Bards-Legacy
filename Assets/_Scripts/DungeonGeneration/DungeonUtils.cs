using System.Collections;
using System;
using UnityEngine;

namespace DUtils
{
    public static class DungeonUtils
    {
        public enum TileType {Floor, Wall}
        
        public static string getRoomKey(TileGrid<Tile> tiles) {
            string output = "";

            output += tiles.Width + ",";
            output += tiles.Height + ",";
            output += tiles.Scale + ",";

            for (int x = 0; x < tiles.Width; x++)
            {
                for (int y = 0; y < tiles.Height; y++)
                {
                    if(x == tiles.Width - 1 && y == tiles.Height - 1) {
                        output += ((int) tiles.GetValue(x, y).tileType);
                    } else {
                        output += ((int) tiles.GetValue(x, y).tileType) + ",";
                    }
                }
            }

            return output;
        }

        public static TileGrid<Tile> getRoomGrid(string roomKey) {
            string[] key = roomKey.Split(',');

            TileGrid<Tile> tiles = new TileGrid<Tile>(Int32.Parse(key[0]), Int32.Parse(key[1]), Int32.Parse(key[2]));

            for(int x = 0; x < Int32.Parse(key[0]); x++) {
                for(int y = 0; y < Int32.Parse(key[1]); y++) {
                    int index = (x * Int32.Parse(key[0])) + y + 3;
                    tiles.SetValue(x, y, new Tile(Int32.Parse(key[index])));
                }
            }

            return tiles;
        }
    }
    
}

