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
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0.0f;

            Destroy(room.GetValue(worldPos));
            room.SetValue(worldPos, Instantiate(Resources.Load<GameObject>("TilePrefabs/TileWall")));
        }

        if(Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0.0f;

            Destroy(room.GetValue(worldPos));
            room.SetValue(worldPos, Instantiate(Resources.Load<GameObject>("TilePrefabs/TileFloor")));
        }
    }

    private void setupTileGrid() {
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                room.SetValue(x, y, Instantiate(Resources.Load<GameObject>("TilePrefabs/TileFloor")));
            }
        }
    }
}
