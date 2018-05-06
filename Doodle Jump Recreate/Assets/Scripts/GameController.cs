using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Vector2 spawnPos;
    public int offset;
    public GameObject playerPrefab;
    public GameObject platformPrefab;

    float currentPlatformY;

    Pooler pooler;

	void Start () {
        pooler = Pooler.Instance;

        spawnPlayer();
	}

    void Update() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        float x = Random.Range(-worldScreenWidth / 2, worldScreenWidth / 2);
        float y = Mathf.Pow(player.GetComponent<PlayerController>().bounceForce, 2) /
            (-2 * Physics2D.gravity.y) - 1;
        y = Random.Range(y - 2, y);

        float screenTop = GameObject.FindGameObjectWithTag("MainCamera").transform.position.y + 
            worldScreenHeight / 2;

        if (currentPlatformY + y < screenTop) {
            pooler.spawn("Platform", new Vector2(x, currentPlatformY + y));
            currentPlatformY += y;
        }
    }

    void spawnPlayer() {
        Instantiate(playerPrefab, spawnPos, Quaternion.identity);

        spawnPlatform(spawnPos - new Vector2(0, offset));

        currentPlatformY = spawnPos.y - offset;
    }

    public void spawnPlatform(Vector2 pos) {
        pooler.spawn("Platform", pos);
    }
}
