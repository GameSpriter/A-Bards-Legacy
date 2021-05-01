using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRefs : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        if(player == null) {
            GameObject.FindGameObjectWithTag("Player");
        }
    }
}
