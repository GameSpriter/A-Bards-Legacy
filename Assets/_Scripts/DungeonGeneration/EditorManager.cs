using System;
using UnityEngine;
using UnityEditor;
using DUtils;

public class EditorManager : MonoBehaviour
{

    #region Prefabs
    public string prefabFolderPath = "TilePrefabs";
    public string[] prefabTiles;
    #endregion

    #region Grid Info
    public int gridX = 15;
    public int gridY = 15;
    private TileGrid<GameObject> gameObjects;
    private TileGrid<Tile> tiles;
    #endregion

    private KeyCode activeKey;
    private DungeonUtils.TileType activeTileType;
    private DungeonUtils.TileType eraserTileType;
    public string roomKey = "";

    private void Start()
    {
        gameObjects = new TileGrid<GameObject>(gridX, gridY, 1);
        tiles = new TileGrid<Tile>(gridX, gridY, -1);

        activeKey = KeyCode.Alpha1;
        updateActiveTileType();
        updateEraserTileType();

        setupTileGrid();
    }

    private void Update() {

        #region Mouse Clicks
        if(Input.GetMouseButton(0)) //Left click
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0.0f;

            int x, y;
            gameObjects.GetXY(worldPos, out x, out y);
            if(x >= 0 && x < gridX && y >= 0 && y < gridY)
            {
                if(tiles.GetValue(x,y).tileType != activeTileType) {

                    Destroy(gameObjects.GetValue(worldPos));

                    gameObjects.SetValue(worldPos, Instantiate(getActivePrefab(), new Vector2(x, y), Quaternion.identity));
                    gameObjects.GetValue(worldPos).transform.parent = gameObject.transform;

                    tiles.GetValue(worldPos).tileType = activeTileType;
                }
            }
        }

        if(Input.GetMouseButton(1)) //Right click
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0.0f;
            
            int x, y;
            gameObjects.GetXY(worldPos, out x, out y);
            if(x >= 0 && x < gridX && y >= 0 && y < gridY)
            {
                if(tiles.GetValue(x,y).tileType != eraserTileType) {
                    Destroy(gameObjects.GetValue(worldPos));

                    gameObjects.SetValue(worldPos, Instantiate(Resources.Load<GameObject>(prefabFolderPath + "/" + prefabTiles[0]), new Vector2(x, y), Quaternion.identity));
                    gameObjects.GetValue(worldPos).transform.parent = gameObject.transform;

                    tiles.GetValue(worldPos).tileType = eraserTileType;
                }
            }
        }
        #endregion

        #region Active Key Change
        if(Input.GetKeyDown(KeyCode.Alpha0)) {
            activeKey = KeyCode.Alpha0;
            updateActiveTileType();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            activeKey = KeyCode.Alpha1;
            updateActiveTileType();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            activeKey = KeyCode.Alpha2;
            updateActiveTileType();
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)) {
            activeKey = KeyCode.Alpha3;
            updateActiveTileType();
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)) {
            activeKey = KeyCode.Alpha4;
            updateActiveTileType();
        }
        #endregion
    }

    private void setupTileGrid() {
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                //Tiles
                tiles.SetValue(x, y, new Tile());

                //GameObjects
                gameObjects.SetValue(x, y, Instantiate(Resources.Load<GameObject>(prefabFolderPath + "/" + prefabTiles[0]), new Vector2(x, y), Quaternion.identity));
                gameObjects.GetValue(x, y).transform.parent = gameObject.transform;
            }
        }
    }

    public void setTiles(TileGrid<Tile> tiles) {
        //Validity checking to ensure the width, height, and scale are the same
        if(this.tiles.Width == tiles.Width && this.tiles.Height == tiles.Height && this.tiles.Scale == tiles.Scale) {
            
            for(int x = 0; x < tiles.Width; x++) {
                for(int y = 0; y < tiles.Height; y++) {
                    if(this.tiles.GetValue(x, y).tileType != tiles.GetValue(x, y).tileType) {
                        this.tiles.SetValue(x, y, tiles.GetValue(x, y));
                        for(int i = 0; i < prefabTiles.Length; i++) {
                            GameObject go = Instantiate(Resources.Load<GameObject>(prefabFolderPath + "/" + prefabTiles[i]));
                            if(go.GetComponent<TileGameObject>().tileType == this.tiles.GetValue(x, y).tileType) {
                                Destroy(gameObjects.GetValue(x, y));
                                
                                gameObjects.SetValue(x, y, Instantiate(Resources.Load<GameObject>(prefabFolderPath + "/" + prefabTiles[i]), new Vector2(x, y), Quaternion.identity));
                                gameObjects.GetValue(x, y).transform.parent = gameObject.transform;

                                Destroy(go);
                                break;
                            } else {
                                Destroy(go);
                            }
                        }
                    }
                }
            }
        } else {
            Debug.Log("Error: tiles import does not have the same dimensions as the current set of tiles. Stop running the game and set the grid width and height to the same width and height as the tiles you wish to import.");
        }
    }

    private int getActivePrefabInt() {
        switch (activeKey)
        {
            case KeyCode.Alpha0:
                return 0;
            case KeyCode.Alpha1:
                return 1;
            case KeyCode.Alpha2:
                return 2;
            case KeyCode.Alpha3:
                return 3;
            case KeyCode.Alpha4:
                return 4;
            default:
                return 0;
        }
    }

    private string getActivePrefabString() {
        return prefabTiles[getActivePrefabInt()];
    }

    private GameObject getActivePrefab(){
        return Resources.Load<GameObject>(prefabFolderPath + "/" + getActivePrefabString());
    }

    private void updateActiveTileType() {
        GameObject go = Instantiate(getActivePrefab());
        activeTileType = go.GetComponent<TileGameObject>().tileType;
        Destroy(go);
    }

    private void updateEraserTileType() {
        GameObject go = Resources.Load<GameObject>(prefabFolderPath + "/" + prefabTiles[0]);
        eraserTileType = go.GetComponent<TileGameObject>().tileType;
    }

    public void updateRoomKey() {
        roomKey = DUtils.DungeonUtils.getRoomKey(tiles);
    }
}


[CustomEditor(typeof(EditorManager))]
 class EditorManagerEditor : Editor{
    private SerializedProperty roomKey;

    private void OnEnable() {
        roomKey = serializedObject.FindProperty("roomKey");
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        if(GUILayout.Button("Import")) {
            (target as EditorManager).setTiles(DungeonUtils.getRoomGrid(roomKey.stringValue));
        }
            
        if(GUILayout.Button("Export")) {
            (target as EditorManager).updateRoomKey();
        }
    }
 }