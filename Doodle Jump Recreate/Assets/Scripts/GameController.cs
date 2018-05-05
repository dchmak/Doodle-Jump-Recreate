using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Vector2 spawnPos;
    public int offset;
    public GameObject playerPrefab;
    public GameObject platformPrefab;

    Pooler pooler;

	void Start () {
        pooler = Pooler.Instance;

        spawnPlayer();
        spawnPlatform();
	}

    void spawnPlayer() {
        Instantiate(playerPrefab, 
            spawnPos, 
            Quaternion.identity);
    }

    void spawnPlatform() {
        pooler.spawn("Platform",
            spawnPos - new Vector2(0, offset),
            Quaternion.identity);

        pooler.spawn("Platform", spawnPos + new Vector2(0, offset), Quaternion.identity);
    }
}
