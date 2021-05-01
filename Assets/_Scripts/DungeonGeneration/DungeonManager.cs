using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public int roomsX = 3;
    public int roomsY = 1;
    private int gridX;
    private int gridY;
    private int gridScale = 3;

    public GameObject prefabStartRoom;
    public GameObject prefabEndRoom;
    public GameObject prefabPlaceholder;
    public GameObject prefabWall;
    public GameObject prefabDoor;
    public GameObject prefabFilledRoom; 
    public GameObject[] prefabRooms;

    private TileGrid<GameObject> dungeon;

    private void Start()
    {
        roomsX += 2; //Added to allow room for the start and end room

        gridX = 1 + (roomsX * 6);
        gridY = 1 + (roomsY * 6);

        dungeon = new TileGrid<GameObject>(gridX, gridY, gridScale);

        setupDungeon();
    }

    //Un-comment for debugging purposes ONLY
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Generating New Dungeon");
            for (int x = 0; x < gridX; x++)
            {
                for (int y = 0; y < gridY; y++)
                {
                    Destroy(dungeon.GetValue(x,y));
                }
            }

            setupDungeon();
        }
    }

    private void setupDungeon() {

        //Position control setup
        int start = Random.Range(0, roomsY);
        int end = Random.Range(0, roomsY);
        Debug.Log("Start: 0, " + start);
        Debug.Log("End: " + roomsX + ", " + end);

        //Move the player to the correct position in the dungeon
        Camera.main.GetComponent<CameraRefs>().player.transform.SetPositionAndRotation(new Vector2(10.5f, (start * 18) + 10.5f), Quaternion.identity);

        TileGrid<char> path = setupPath(start, end);

        for (int gx = 0, px = 0; gx < gridX; gx++)
        {
            px = Mathf.FloorToInt(gx / 6.0f);
            for (int gy = 0, py = 0; gy < gridY; gy++)
            {
                py = Mathf.FloorToInt(gy / 6.0f);

                if(isPlaceholder(gx, gy)) {
                    placePlaceholder(gx, gy);
                } else if(isRoom(gx, gy)) {
                    if(path.GetValue(px, py).Equals('Z')) {
                        placeFilledRoom(gx, gy);
                    } else if(path.GetValue(px, py).Equals('B')) {
                        placeStartRoom(gx, gy);
                    } else if(path.GetValue(px, py).Equals('F')) {
                        placeEndRoom(gx, gy);
                    } else {
                        placeRandomRoom(gx, gy);
                    }
                } else if(isSolidWall(gx, gy)) {
                    placeSolidWall(gx, gy);
                } else if(isDoorway(gx, gy, path, px, py)) {
                    placeOpenDoorway(gx, gy);
                } else {
                    placeSolidWall(gx, gy);
                }
            }
        }
    }

    private void placePlaceholder(int x, int y) {
        dungeon.SetValue(x, y, Instantiate(prefabPlaceholder, new Vector2(x * dungeon.scale, y * dungeon.scale), Quaternion.identity));
        dungeon.GetValue(x, y).transform.parent = gameObject.transform;
        dungeon.GetValue(x, y).name = "Placeholder: (" + x + ", " + y + ")";
    }
    private void placeRandomRoom(int x, int y) {
        int index = Random.Range(0, prefabRooms.Length);
        
        dungeon.SetValue(x, y, Instantiate(prefabRooms[index], new Vector2(x * dungeon.scale, y * dungeon.scale), Quaternion.identity));
        dungeon.GetValue(x, y).transform.parent = gameObject.transform;
        dungeon.GetValue(x, y).name = "Room: (" + x + ", " + y + ")";
    }
    private void placeStartRoom(int x, int y) {
        dungeon.SetValue(x, y, Instantiate(prefabStartRoom, new Vector2(x * dungeon.scale, y * dungeon.scale), Quaternion.identity));
        dungeon.GetValue(x, y).transform.parent = gameObject.transform;
        dungeon.GetValue(x, y).name = "Starting Room: (" + x + ", " + y + ")";
    }
    private void placeEndRoom(int x, int y) {
        dungeon.SetValue(x, y, Instantiate(prefabEndRoom, new Vector2(x * dungeon.scale, y * dungeon.scale), Quaternion.identity));
        dungeon.GetValue(x, y).transform.parent = gameObject.transform;
        dungeon.GetValue(x, y).name = "Starting Room: (" + x + ", " + y + ")";
    }
    private void placeFilledRoom(int x, int y) {
        int index = Random.Range(0, prefabRooms.Length);
        
        dungeon.SetValue(x, y, Instantiate(prefabFilledRoom, new Vector2(x * dungeon.scale, y * dungeon.scale), Quaternion.identity));
        dungeon.GetValue(x, y).transform.parent = gameObject.transform;
        dungeon.GetValue(x, y).name = "Filled Room: (" + x + ", " + y + ")";
    }
    private void placeSolidWall(int x, int y) {
        dungeon.SetValue(x, y, Instantiate(prefabWall, new Vector2(x * dungeon.scale, y * dungeon.scale), Quaternion.identity));
        dungeon.GetValue(x, y).transform.parent = gameObject.transform;
        dungeon.GetValue(x, y).name = "Wall: (" + x + ", " + y + ")";
    }
    private void placeOpenDoorway(int x, int y) {
        dungeon.SetValue(x, y, Instantiate(prefabDoor, new Vector2(x * dungeon.scale, y * dungeon.scale), Quaternion.identity));
        dungeon.GetValue(x, y).transform.parent = gameObject.transform;
        dungeon.GetValue(x, y).name = "Doorway: (" + x + ", " + y + ")";
    }

    private bool isPlaceholder(int x, int y) {
        if(x % 6 != 0 && y % 6 != 0) {//is placeholder if it's not a room
            if (x % 6 == 1 && y % 6 == 1) {//Is room
                return false;
            } else {
                return true;
            }
        }

        return false;
    }
    private bool isRoom(int x, int y) {
        if(x % 6  == 1 && y % 6 == 1) {
            return true;
        }

        return false;
    }
    private bool isSolidWall(int x, int y) {
        if(x == 0 || y == 0 || x == gridX - 1 || y == gridY - 1) {//Litterally edge cases; They're on the edge of the map
            return true;
        }
        
        if(x % 6 == 0 && y % 6 == 0) { //Corner
            return true;
        }

        if(x % 6 == 1 || x % 6 == 5) {
            if(y % 6 == 1) { //One off of the corner horizontally
                return true;
            }
        }

        if(y % 6 == 1 || y % 6 == 5) {
            if(x % 6 == 1) { //One off of the corner vertically
                return true;
            }
        }

        return false;
    }
    private bool isDoorway(int x, int y, TileGrid<char> path, int px, int py) {
        if(x == 0 || x == gridX) {
            return false;
        }

        if(y == 0 || y == gridY) {
            return false;
        }

        if(y % 6 == 3) {
            if(path.GetValue(px - 1, py).Equals('E') || path.GetValue(px - 1, py).Equals('B')) { //If the path to the left of the doorway leads easy
                return true;
            } else if (!path.GetValue(px - 1, py).Equals('Z') && !path.GetValue(px, py).Equals('Z')) {//If the path to the left and right are both valid rooms
                int temp = Random.Range(0, 3);
                if(temp == 1) { //1 in 3 chances it will be an open doorway
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }

        if(x % 6 == 3) {
            if(path.GetValue(px, py - 1).Equals('N') || path.GetValue(px, py).Equals('S')) { //If the path to the left of the doorway leads easy
                return true;
            } else if (!path.GetValue(px, py - 1).Equals('Z') && !path.GetValue(px, py).Equals('Z')) {//If the path to the left and right are both valid rooms
                int temp = Random.Range(0, 3);
                if(temp == 0) { //1 in 3 chances it will be an open doorway
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }

        return false;
    }

    private TileGrid<char> setupPath(int start, int end) {

        int posX = 0;
        int posY = start;

        //Path grid setup
        TileGrid<char> path = new TileGrid<char>(roomsX, roomsY, 1);

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
