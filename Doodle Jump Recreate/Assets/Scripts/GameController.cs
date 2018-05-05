using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Vector2 spawnPos;
    public GameObject playerPrefab;
    public GameObject platformPrefab;

	void Start () {
        spawnPlayer();
        spawnPlatform();
	}

    void spawnPlayer() {
        Instantiate(playerPrefab, spawnPos, Quaternion.identity);
    }

    void spawnPlatform() {

    }
}
