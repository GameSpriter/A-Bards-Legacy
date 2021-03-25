using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows notes to repeatly shift and despawn with triggers
/// </summary>
public class NoteTriggers : MonoBehaviour
{
    public NoteSpawn ns;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Shifter") && collision.gameObject.CompareTag("Note"))
        {
            ns.endIsReached = true;
            ns.noteShift();
        }
        if (gameObject.CompareTag("Despawner") && collision.gameObject.CompareTag("Note"))
        {
            ns.despawn();
        }
    }
}
