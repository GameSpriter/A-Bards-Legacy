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
    public bool endIsReached = false;

    // Start is called before the first frame update
    void Start()
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
        if (!mc[0].isHit && mc[0].GetInputNoteName == "Up Note") 
        {
            mc[0].isHit = true;
            noteSpawn(0);
        }
        else if (!mc[1].isHit && mc[1].GetInputNoteName == "Right Note")
        {
            mc[1].isHit = true;
            noteSpawn(1);
        }
        else if (!mc[2].isHit && mc[2].GetInputNoteName == "Left Note")
        {
            mc[2].isHit = true;
            noteSpawn(2);
        }
        else if (!mc[3].isHit && mc[3].GetInputNoteName == "Down Note")
        {
            mc[3].isHit = true;
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
        if (!endIsReached)
        {
            if (incrementPosition < 8.0f)
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
    public void despawn()
    {
        Destroy(tempSpawnContainer[0]);
        tempSpawnContainer.RemoveAt(0);        
    }
}
