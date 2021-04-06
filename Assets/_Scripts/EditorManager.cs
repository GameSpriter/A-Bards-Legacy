using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    private TileGrid room;
    private TileGrid dungeon;

    private void Start()
    {
        room = new TileGrid(15, 15, 1);
        dungeon = new TileGrid(5, 5, 15);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0.0f;

            room.SetTile(worldPos, TileGrid.TileType.Wall);
            dungeon.SetTile(worldPos, TileGrid.TileType.Floor);
        }
    }
}
