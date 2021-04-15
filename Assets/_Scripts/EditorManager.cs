using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    public int gridX = 15;
    public int gridY = 15;

    private TileGrid<GameObject> room;

    private void Start()
    {
        room = new TileGrid<GameObject>(gridX, gridY, 1);

        setupTileGrid();
    }

    private void Update()
    {

        if(Input.GetMouseButton(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0.0f;

            Destroy(room.GetValue(worldPos));
            int x, y;
            room.GetXY(worldPos, out x, out y);
            if(x >= 0 && x < gridX && y >= 0 && y < gridY)
            {
                room.SetValue(worldPos, Instantiate(Resources.Load<GameObject>("TilePrefabs/TileWall"), new Vector2(x, y), Quaternion.identity));
                room.GetValue(worldPos).transform.parent = gameObject.transform;
            }
        }

        if(Input.GetMouseButton(1))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0.0f;

            Destroy(room.GetValue(worldPos));
            int x, y;
            room.GetXY(worldPos, out x, out y);
            if(x >= 0 && x < gridX && y >= 0 && y < gridY)
            {
                room.SetValue(worldPos, Instantiate(Resources.Load<GameObject>("TilePrefabs/TileFloor"), new Vector2(x, y), Quaternion.identity));
                room.GetValue(worldPos).transform.parent = gameObject.transform;
            }
        }
    }

    private void setupTileGrid() {
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                room.SetValue(x, y, Instantiate(Resources.Load<GameObject>("TilePrefabs/TileFloor"), new Vector2(x, y), Quaternion.identity));
                room.GetValue(x, y).transform.parent = gameObject.transform;
            }
        }
    }
}
