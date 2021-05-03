using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerPos;

    private void Awake() {
        if(playerPos == null) {
            playerPos = Camera.main.GetComponent<CameraRefs>().player.transform;
        }
    }

    void Update()
    {
        transform.position = new Vector3(playerPos.position.x, playerPos.position.y, -10);
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, ))
    }
}
