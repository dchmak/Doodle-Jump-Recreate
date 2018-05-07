using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    GameObject player;
	
	private void LateUpdate () {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        } else {
            if (transform.position.y < player.transform.position.y) {
                transform.position = new Vector3(0, player.transform.position.y, -10);
            }
        }
	}
}
