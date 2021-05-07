using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollision : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("TileWall"))
        {
            Destroy(this.gameObject, 0f);
        }
    }
}
