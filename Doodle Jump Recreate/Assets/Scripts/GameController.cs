using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Vector2 spawnPos;
    public int offset;
    public GameObject playerPrefab;
    public GameObject platformPrefab;
    public int visiblePlatform;

    int currentVisiblePlatform;

    Pooler pooler;

	void Start () {
        pooler = Pooler.Instance;

        spawnPlayer();

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        for (int i = currentVisiblePlatform; i < visiblePlatform; i++) {
            spawnPlatform(new Vector2(Random.Range(-worldScreenWidth / 2, worldScreenWidth / 2),
                Random.Range(-worldScreenHeight / 2, worldScreenHeight)));
        }
	}

    void spawnPlayer() {
        Instantiate(playerPrefab, spawnPos, Quaternion.identity);

        spawnPlatform(spawnPos - new Vector2(0, offset));

        currentVisiblePlatform = 1;
    }

    void spawnPlatform(Vector2 pos) {
        pooler.spawn("Platform", pos);            

        currentVisiblePlatform++;
    }
}
