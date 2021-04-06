using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawn : MonoBehaviour
{
    public HamonicsDisplay hd;
    public MouseOnInputNote[] mc;

    public GameObject[] spawnPositions;
    public GameObject[] notes;
    public List<GameObject> tempSpawnContainer = new List<GameObject>();

    public float incrementPosition = 0.0f;
    private float offset = 9.0f;

    public bool EndIsReached { get; set; } = false;

    // Start is called before the first frame update
    void Awake()
    {
        //Set notes in designated positions on music sheet
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].transform.position = spawnPositions[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Playing a note will happen once when touched
        if (!mc[0].IsHit && mc[0].GetInputNoteName == "Up Button") 
        {
            mc[0].IsHit = true;
            noteSpawn(0);
        }
        else if (!mc[1].IsHit && mc[1].GetInputNoteName == "Right Button")
        {
            mc[1].IsHit = true;
            noteSpawn(1);
        }
        else if (!mc[2].IsHit && mc[2].GetInputNoteName == "Left Button")
        {
            mc[2].IsHit = true;
            noteSpawn(2);
        }
        else if (!mc[3].IsHit && mc[3].GetInputNoteName == "Down Button")
        {
            mc[3].IsHit = true;
            noteSpawn(3);
        }
        
    }

    //How notes will be displayed in proper positions
    void noteSpawn(int i) 
    {
        tempSpawnContainer.Add(Instantiate(notes[i]));
        tempSpawnContainer[tempSpawnContainer.Count - 1].transform.position =
            new Vector3(tempSpawnContainer[tempSpawnContainer.Count - 1].transform.position.x + incrementPosition,
            tempSpawnContainer[tempSpawnContainer.Count - 1].transform.position.y);
        if (!EndIsReached)
        {
            if (incrementPosition < transform.position.x + offset)
            {
                incrementPosition += 1.0f;
            }
        }
    }

    //Once notes reach past the front of music sheet, they all move back to make room for incoming notes
    public void noteShift()
    {
        for (int i = 0; i < tempSpawnContainer.Count; i++) 
        {
            //tempSpawnContainer[i].transform.Translate(new Vector3(tempSpawnContainer[i].transform.position.x - 1.0f,
            //tempSpawnContainer[i].transform.position.y));
            tempSpawnContainer[i].transform.position = new Vector3(tempSpawnContainer[i].transform.position.x - 1.0f,
                tempSpawnContainer[i].transform.position.y);
        }
    }

    //Once a note reach the back end of the music sheet they disappear
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
