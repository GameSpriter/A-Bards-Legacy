using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRefs : MonoBehaviour
{
    public GameObject player;
    
    private void Awake()
    {
        if(player == null) {
            GameObject.FindGameObjectWithTag("Player");
        }
    }
}
