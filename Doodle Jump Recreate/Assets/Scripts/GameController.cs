using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Vector2 spawnPos;
    public int offset;
    public GameObject playerPrefab;
    public GameObject platformPrefab;
    public Text score;

    public static float maxDistanceTravelled;

    float currentPlatformY;

    Pooler pooler;
    GameObject player;

    float worldScreenHeight, worldScreenWidth;
    float platformWidth;
    float spawnX, spawnY;

    void Start () {
        pooler = Pooler.Instance;

        worldScreenHeight = Camera.main.orthographicSize * 2f;
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        platformWidth = platformPrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        spawnPlayer();

        maxDistanceTravelled = 0f;
    }

    void Update() {
        if (player != null) {
            float top = GameObject.FindGameObjectWithTag("MainCamera").transform.position.y +
                worldScreenHeight * 3 / 2;

            spawnX = Random.Range(-worldScreenWidth / 2 + platformWidth / 2,
                worldScreenWidth / 2 - platformWidth / 2);
            spawnY = Mathf.Pow(player.GetComponent<PlayerController>().bounceForce, 2) /
                (-2 * Physics2D.gravity.y * player.GetComponent<Rigidbody2D>().gravityScale) - 1;
            //y = Random.Range(y - 2, y);

            if (currentPlatformY + spawnY < top) {
                pooler.spawn("Platform", new Vector2(spawnX, currentPlatformY + spawnY));
                currentPlatformY += spawnY;
            }
        }

        maxDistanceTravelled = Mathf.Max(maxDistanceTravelled,
            player.transform.position.y);
        score.text = "Score: " + (maxDistanceTravelled * 10).ToString("F0");
    }

    void spawnPlayer() {
        player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);

        spawnPlatform(spawnPos - new Vector2(0, offset));

        currentPlatformY = spawnPos.y - offset;
    }

    public void spawnPlatform(Vector2 pos) {
        pooler.spawn("Platform", pos);
    }
}
