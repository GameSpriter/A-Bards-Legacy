using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public int gridX = 3;
    public int gridY = 1;

    public GameObject[] prefabRooms;

    private TileGrid<GameObject> dungeon;

    private void Start()
    {
        dungeon = new TileGrid<GameObject>(gridX, gridY, 15);

        setupDungeon();
    }

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
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                int index = Random.Range(0, prefabRooms.Length);
                
                dungeon.SetValue(x, y, Instantiate(prefabRooms[index], new Vector2(x * dungeon.scale, y * dungeon.scale), Quaternion.identity));
                dungeon.GetValue(x, y).transform.parent = gameObject.transform;
            }
        }
    }
}
