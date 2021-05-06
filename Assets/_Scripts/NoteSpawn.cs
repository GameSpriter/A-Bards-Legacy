using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Set's up how notes are controlled and displayed in scene
/// </summary>
public class NoteSpawn : MonoBehaviour
{
    public MouseOnInputNote[] mc;

    public GameObject[] spawnPositions;

    //[SerializeField]
    //private Transform[] spawnTransforms;
    
    public GameObject[] notes;
    public List<GameObject> tempSpawnContainer = new List<GameObject>();

    public float incrementPosition = 0.0f;
    private float offset = 9f;
    private float shift = 0.25f;

    public bool EndIsReached { get; set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {     
        //Playing a note will happen once when touched
        if (!mc[0].IsHit && mc[0].GetInputNoteName == "Up Button") 
        {
            mc[0].IsHit = true;
            NoteSpawner(0);
        }
        else if (!mc[1].IsHit && mc[1].GetInputNoteName == "Right Button")
        {
            mc[1].IsHit = true;
            NoteSpawner(1);
        }
        else if (!mc[2].IsHit && mc[2].GetInputNoteName == "Left Button")
        {
            mc[2].IsHit = true;
            NoteSpawner(2);
        }
        else if (!mc[3].IsHit && mc[3].GetInputNoteName == "Down Button")
        {
            mc[3].IsHit = true;
            NoteSpawner(3);
        }
        
    }

    private void FixedUpdate()
    {
        //Set notes in designated positions on music sheet
        for (int i = 0; i < notes.Length; i++)
        {
            //notes[i].transform.position = spawnPositions[i].transform.position;
        }
    }

    /// <summary>
    /// How notes will be displayed in proper positions
    /// </summary>
    /// <param name="i"></param>
    void NoteSpawner(int i) 
    {
        tempSpawnContainer.Add(Instantiate(notes[i], spawnPositions[i].transform.position, Quaternion.identity));
        tempSpawnContainer[tempSpawnContainer.Count - 1].transform.SetParent(gameObject.transform);
        tempSpawnContainer[tempSpawnContainer.Count - 1].transform.position =
            new Vector3(tempSpawnContainer[tempSpawnContainer.Count - 1].transform.position.x + incrementPosition,
            tempSpawnContainer[tempSpawnContainer.Count - 1].transform.position.y);
        if (!EndIsReached)
        {
            if (incrementPosition < transform.position.x + offset)
            {
                incrementPosition += (shift);
            }
        }
    }

    /// <summary>
    /// Once notes reach past the front of music sheet, they all move back to make room for incoming notes
    /// </summary>
    public void NoteShift()
    {
        for (int i = 0; i < tempSpawnContainer.Count; i++) 
        {
            //tempSpawnContainer[i].transform.Translate(new Vector3(tempSpawnContainer[i].transform.position.x - 1.0f,
            //tempSpawnContainer[i].transform.position.y));
            tempSpawnContainer[i].transform.position = new Vector3(tempSpawnContainer[i].transform.position.x - shift,
                tempSpawnContainer[i].transform.position.y);
        }
    }

    /// <summary>
    /// Once a note reach the back end of the music sheet they disappear
    /// </summary>
    public void Despawn()
    {
        Destroy(tempSpawnContainer[0]);
        tempSpawnContainer.RemoveAt(0);        
    }

    public void NoteSpawnReset() 
    {
        for (int i = 0; i < tempSpawnContainer.Count; i++)
        {
            Destroy(tempSpawnContainer[i]);
        }

        tempSpawnContainer.Clear();

        EndIsReached = false;
        incrementPosition = 0;
    }
}
