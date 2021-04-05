using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to give this note an identity in NoteSpawn
/// </summary>
public class MouseOnInputNote : MonoBehaviour
{
<<<<<<< Updated upstream
    public Sprite selectSprite;
    private Sprite spriteOriginal;

    public string GetInputNoteName { get; set; }
    public bool IsHit { get; set; } = false;

    private void Start()
    {
        spriteOriginal = gameObject.GetComponent<SpriteRenderer>().sprite;
    }
=======
    private string getInputNoteName;
    public bool isHit = false;

    public string GetInputNoteName { get => getInputNoteName; set => getInputNoteName = value; }
>>>>>>> Stashed changes

    private void OnMouseOver()
    {
        GetInputNoteName = gameObject.name;
<<<<<<< Updated upstream
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
=======
>>>>>>> Stashed changes
    }

    private void OnMouseExit()
    {
        GetInputNoteName = "";
<<<<<<< Updated upstream
        IsHit = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteOriginal;
=======
        isHit = false;
>>>>>>> Stashed changes
    }
}
