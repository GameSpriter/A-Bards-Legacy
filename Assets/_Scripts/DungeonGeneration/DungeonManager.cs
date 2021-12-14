using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DUtils;

public class DungeonManager : MonoBehaviour
{
    #region Room/Grid Vars
    public int roomsX = 3;
    public int roomsY = 1;
    private int gridX;
    private int gridY;
    private int wallThickness = 3;
    private int roomWidth;
    #endregion

    #region Enemies
    public float EnemySpawnChance = 0.005f;
    public GameObject PrefabEnemyChompyBoy;
    public GameObject PrefabEnemyQuarterChompy;
    public float EnemyQuaterChompySpawnChance = 0.25f;
    #endregion

    #region Prefabs
    public GameObject prefabWall;
    public GameObject prefabFloor;
    public GameObject prefabExit;
    #endregion

    #region Scriptable Objects
    public ScriptableRoom scriptableStartRoom;
    public ScriptableRoom scriptableEndRoom;
    public ScriptableRoom[] scriptableRooms;
    #endregion

    #region TileGrid Tiles
    private TileGrid<Tile> tileGridStartRoom;
    private TileGrid<Tile> tileGridEndRoom;
    private TileGrid<Tile>[] tileGridRooms;
    #endregion

    private TileGrid<GameObject> dungeon;

    private void Start() {
        roomsX += 2; //Added to allow room for the start and end room

        //Convert from Scriptable Objects to TileGrids
        tileGridStartRoom = DungeonUtils.getRoomGrid(scriptableStartRoom.Key);
        tileGridEndRoom = DungeonUtils.getRoomGrid(scriptableEndRoom.Key);
        tileGridRooms = new TileGrid<Tile>[scriptableRooms.Length];
        for(int i = 0; i < scriptableRooms.Length; i++) {
            tileGridRooms[i] = DungeonUtils.getRoomGrid(scriptableRooms[i].Key);
        }

        roomWidth = tileGridRooms[0].Width;

        gridX = ((roomsX + 1) * roomWidth) + wallThickness;
        gridY = (roomsY + 1) * roomWidth + wallThickness;

        dungeon = new TileGrid<GameObject>(gridX, gridY, 1);

        setupDungeon();
    }

    private void setupDungeon() {

        //Position control setup
        int start = Random.Range(0, roomsY);
        int end = Random.Range(0, roomsY);
        Debug.Log("Start: 0, " + start);
        Debug.Log("End: " + (roomsX - 1) + ", " + end);

        //Move the player to the correct position in the dungeon
        Camera.main.GetComponent<CameraRefs>().player.transform.SetPositionAndRotation(new Vector2((0.5f * roomWidth) + wallThickness, (start * (roomWidth + wallThickness)) + (0.5f * roomWidth) + wallThickness), Quaternion.identity);

        TileGrid<char> path = setupPath(start, end);
        bool generating = true;
        int pathX = 0;
        int pathY = start;

        while(generating) {
            if(path.GetValue(pathX, pathY) == 'B') {
                placeSouthWall(pathX, pathY);           //South Wall
                placeWestWall(pathX, pathY);            //West Wall
                placeSouthWall(pathX, pathY + 1);       //North Wall
                placeWestWall(pathX + 1, pathY, true);  //East Wall

                placeRoom(pathX, pathY, tileGridStartRoom);

                pathX++;
            } else if(path.GetValue(pathX, pathY) == 'N') {
                if(path.GetValue(pathX, pathY - 1) != 'N') {//South Wall
                    placeSouthWall(pathX, pathY);
                }
                if(path.GetValue(pathX - 1, pathY) == 'Z') {//West Wall
                    placeWestWall(pathX, pathY);
                }
                placeSouthWall(pathX, pathY + 1, true);     //North Wall
                if(path.GetValue(pathX + 1, pathY) != 'Z') {//East Wall; 1 in 3 chance to place a wall with a door between two rooms
                    if(Random.Range(0, 3) == 0) {
                        placeWestWall(pathX + 1, pathY, true);
                    } else {
                        placeWestWall(pathX + 1, pathY);
                    }
                } else {
                    placeWestWall(pathX + 1, pathY);
                }
                placeNorthEastCorner(pathX, pathY - 1);
                
                placeRoom(pathX, pathY, tileGridRooms[Random.Range(0, tileGridRooms.Length)]);

                pathY++;
            } else if(path.GetValue(pathX, pathY) == 'E') {
                Debug.Log(string.Format("Room: ({0},{1}); Path: E", pathX, pathY));
                if(path.GetValue(pathX, pathY - 1) != 'N') {//South Wall
                    placeSouthWall(pathX, pathY);
                }
                if(path.GetValue(pathX - 1, pathY) == 'Z') {//West Wall
                    placeWestWall(pathX, pathY);
                }
                if(path.GetValue(pathX, pathY + 1) == 'Z' || path.GetValue(pathX, pathY + 1) == '\0') {//North Wall
                    placeSouthWall(pathX, pathY + 1);
                }
                placeWestWall(pathX + 1, pathY, true);      //East Wall

                placeRoom(pathX, pathY, tileGridRooms[Random.Range(0, tileGridRooms.Length)]);

                pathX++;
            } else if(path.GetValue(pathX, pathY) == 'S') {
                placeSouthWall(pathX, pathY, true);         //South Wall
                if(path.GetValue(pathX - 1, pathY) == 'Z') {//West Wall
                    placeWestWall(pathX, pathY);
                }
                if(path.GetValue(pathX, pathY + 1) != 'S') {//North Wall
                    placeSouthWall(pathX, pathY + 1);
                }
                if(path.GetValue(pathX + 1, pathY) != 'Z') {//East Wall; 1 in 3 chance to place a wall with a door between two rooms
                    if(Random.Range(0, 3) == 0) {
                        placeWestWall(pathX + 1, pathY, true);
                    } else {
                        placeWestWall(pathX + 1, pathY);
                    }
                } else {
                    placeWestWall(pathX + 1, pathY);
                }
                placeNorthEastCorner(pathX, pathY);
                
                placeRoom(pathX, pathY, tileGridRooms[Random.Range(0, tileGridRooms.Length)]);

                pathY--;
            } else if(path.GetValue(pathX, pathY) == 'F') {
                placeSouthWall(pathX, pathY);           //South Wall
                //placeWestWall(pathX, pathY);          //West Wall
                placeSouthWall(pathX, pathY + 1);       //North Wall
                placeWestWall(pathX + 1, pathY);        //East Wall
                
                placeNorthEastCorner(pathX, pathY);
                placeNorthEastCorner(pathX, pathY - 1);

                placeRoom(pathX, pathY, tileGridEndRoom);

                placeExitFlag(pathX, pathY);
                
                break;
            }
        }
        
        GetComponent<TilesetController>().setupTileset(dungeon);
    }

    private void placeRoom(int pathX, int pathY, TileGrid<Tile> tileGridRoom) {
        //For each tile int the room, place a floor or wall tile
        for (int roomX = 0; roomX < roomWidth; roomX++) {
            for (int roomY = 0; roomY < roomWidth; roomY++) {
                int worldX = roomX + getWorldPosFromPath(pathX) + wallThickness;
                int worldY = roomY + getWorldPosFromPath(pathY) + wallThickness;
                
                if(tileGridRoom.GetValue(roomX, roomY).tileType == DungeonUtils.TileType.Wall) {
                    placePrefab(worldX, worldY, prefabWall);
                } else if(tileGridRoom.GetValue(roomX, roomY).tileType == DungeonUtils.TileType.Floor) {
                    placePrefab(worldX, worldY, prefabFloor);

                    spawnEnemy(worldX, worldY);
                }
            }
        }
    }

    private void placeSouthWall(int pathX, int pathY, bool hasDoor = false) {
        for(int worldX = getWorldPosFromPath(pathX); worldX < getWorldPosFromPath(pathX + 1); worldX++) {
            for(int worldY = getWorldPosFromPath(pathY); worldY < getWorldPosFromPath(pathY) + wallThickness; worldY++) {
                if(hasDoor) {
                    int halfWayPoint = (getWorldPosFromPath(pathX) + getWorldPosFromPath(pathX + 1)) / 2;
                    halfWayPoint++;
                    if(worldX >= halfWayPoint - 1 && worldX <= halfWayPoint + 1) {
                        placePrefab(worldX, worldY, prefabFloor);
                    } else {
                        placePrefab(worldX, worldY, prefabWall);
                    }
                } else {
                    placePrefab(worldX, worldY, prefabWall);
                }
            }
        }
    }

    private void placeWestWall(int pathX, int pathY, bool hasDoor = false) {
        for(int worldX = getWorldPosFromPath(pathX); worldX < getWorldPosFromPath(pathX) + wallThickness; worldX++) {
            for(int worldY = getWorldPosFromPath(pathY) + wallThickness; worldY < getWorldPosFromPath(pathY + 1); worldY++) {
                if(hasDoor) {
                    int halfWayPoint = (getWorldPosFromPath(pathY) + getWorldPosFromPath(pathY + 1)) / 2;
                    halfWayPoint++;
                    if(worldY >= halfWayPoint - 1 && worldY <= halfWayPoint + 1) {
                        placePrefab(worldX, worldY, prefabFloor);
                    } else {
                        placePrefab(worldX, worldY, prefabWall);
                    }
                } else {
                    placePrefab(worldX, worldY, prefabWall);
                }
            }
        }
    }

    private void placeNorthEastCorner(int pathX, int pathY) {
        for(int worldX = getWorldPosFromPath(pathX + 1); worldX < getWorldPosFromPath(pathX + 1) + wallThickness; worldX++) {
            for(int worldY = getWorldPosFromPath(pathY + 1); worldY < getWorldPosFromPath(pathY + 1) + wallThickness; worldY++) {
                placePrefab(worldX, worldY, prefabWall);
            }
        }
    }

    private void placeExitFlag(int pathX, int pathY) {
        int worldX = (roomWidth / 2) + getWorldPosFromPath(pathX) + wallThickness;
        int worldY = (roomWidth / 2) + getWorldPosFromPath(pathY) + wallThickness;
        Instantiate(prefabExit, new Vector2(worldX, worldY), Quaternion.identity);
    }

    private int getWorldPosFromPath(int num) {
        return num * (roomWidth + wallThickness);
    }

    private void placePrefab(int x, int y, GameObject prefab) {
        dungeon.SetValue(x, y, Instantiate(prefab, new Vector2(x * dungeon.Scale, y * dungeon.Scale), Quaternion.identity));
        dungeon.GetValue(x, y).transform.parent = gameObject.transform;
        dungeon.GetValue(x, y).name = dungeon.GetValue(x, y).GetComponent<TileGameObject>().tileType + ": (" + x + ", " + y + ")";
    }

    private void spawnEnemy(int x, int y) {
        if(x <= roomWidth + wallThickness) { // Spawn Room Protection
            return;
        }

        if (Random.Range(0.0f, 1.0f) < EnemySpawnChance) {
            if(Random.Range(0.0f, 1.0f) < EnemyQuaterChompySpawnChance) {
                Instantiate(PrefabEnemyQuarterChompy, new Vector2(x, y), Quaternion.identity);
            } else {
                Instantiate(PrefabEnemyChompyBoy, new Vector2(x, y), Quaternion.identity);
            }
        }
    }

    private TileGrid<char> setupPath(int start, int end) {

        int posX = 0;
        int posY = start;

        //Path grid setup
        TileGrid<char> path = new TileGrid<char>(roomsX, roomsY, -1);

        for (int i = 0; i < roomsX; i++) {
            for (int j = 0; j < roomsY; j++) {
                path.SetValue(i, j, 'Z');
            }
        }

        //Start Room
        path.SetValue(posX, posY, 'B');
        posX++;

        //Path creation
        while(posX != roomsX - 2) {
            int nextDir = -1;

            //0 = North; 1 = East; 2 = South;
            if (posY == 0) { //At bottom
                nextDir = Random.Range(0, 2);
            } else if (posY == roomsY - 1) { //At top
                nextDir = Random.Range(1,3);
            } else { //Not at top or bottom; in middle
                nextDir = Random.Range(0,3);
            }

            if(nextDir == 0) { // North
                if (path.GetValue(posX, posY + 1).Equals('Z')) {
                    path.SetValue(posX, posY, 'N');
                    posY += 1;
                }
            } else if(nextDir == 1) { //East
                if (path.GetValue(posX + 1, posY).Equals('Z')) {
                    path.SetValue(posX, posY, 'E');
                    posX += 1;
                }
            } else if(nextDir == 2) { //South
                if (path.GetValue(posX, posY - 1).Equals('Z')) {
                    path.SetValue(posX, posY, 'S');
                    posY += -1;
                }
            }
        }

        while(posY != end) {
            if(posY > end) {
                path.SetValue(posX, posY, 'S');
                posY--;
            } else {
                path.SetValue(posX, posY, 'N');
                posY++;
            }
        }

        path.SetValue(posX, posY, 'E');
        posX++;
        path.SetValue(posX, posY, 'F');

        return path;
    }
}
