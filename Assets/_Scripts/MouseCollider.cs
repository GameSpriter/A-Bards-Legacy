using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Obsolete for now
/// </summary>
public class MouseCollider : MonoBehaviour
{
    public Camera cam;

    public RaycastHit2D hit = new RaycastHit2D();

    public List<GameObject> inputNotes;
    private string getInputNoteName;
    public bool isHit = false;

    public string GetInputNoteName { get => getInputNoteName; set => getInputNoteName = value; }

    private void OnMouseOver()
    {
        
    }

    void Update()
    {
        /*
        hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit)
        {
            if (hit.collider.CompareTag("InputNote"))
            {
                foreach (GameObject note in inputNotes)
                {
                    if (hit.collider.name == note.name) 
                    {
                        GetInputNoteName = note.name;
                    }
                }
            }
        }
        else
        {
            isHit = false;
        }
        */

    }
}
