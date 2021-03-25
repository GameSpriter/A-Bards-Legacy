using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to give this note an identity in NoteSpawn
/// </summary>
public class MouseOnInputNote : MonoBehaviour
{
    private string getInputNoteName;
    public bool isHit = false;

    public string GetInputNoteName { get => getInputNoteName; set => getInputNoteName = value; }

    private void OnMouseOver()
    {
        GetInputNoteName = gameObject.name;
    }

    private void OnMouseExit()
    {
        GetInputNoteName = "";
        isHit = false;
    }
}
