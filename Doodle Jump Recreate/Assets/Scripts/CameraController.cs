using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    GameObject player;
	
	void Update () {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        } else {
            //Debug.Log(player.transform.position.y);

            float yPos = player.transform.position.y;
            if (transform.position.y < yPos) {
                transform.position = new Vector3(0, yPos, -10);
            }
        }
	}
}
