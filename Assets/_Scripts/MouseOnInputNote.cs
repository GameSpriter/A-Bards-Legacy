using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to give this note an identity in NoteSpawn
/// </summary>
public class MouseOnInputNote : MonoBehaviour
{
    public Sprite selectSprite;
    private Sprite spriteOriginal;

    private string getInputNoteName;
    public bool isHit = false;

    public string GetInputNoteName { get => getInputNoteName; set => getInputNoteName = value; }

    private void Start()
    {
        spriteOriginal = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    private void OnMouseOver()
    {
        GetInputNoteName = gameObject.name;
        switch (gameObject.name)
        {
            case "Down Button":
                gameObject.GetComponent<SpriteRenderer>().sprite = selectSprite;
                break;
            case "Up Button":
                gameObject.GetComponent<SpriteRenderer>().sprite = selectSprite;
                break;
            case "Left Button":
                gameObject.GetComponent<SpriteRenderer>().sprite = selectSprite;
                break;
            case "Right Button":
                gameObject.GetComponent<SpriteRenderer>().sprite = selectSprite;
                break;
            default:
                break;
        }
    }

    private void OnMouseExit()
    {
        GetInputNoteName = "";
        isHit = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteOriginal;
    }
}
