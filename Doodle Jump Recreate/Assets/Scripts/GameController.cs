using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [Header("Spawn Positions")]
    public Vector2 spawnPos;
    public int platformOffset;

    [Space]

    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject platformPrefab;
    public GameObject brokenPlatformPrefab;

    [Space]

    [Header("Others")]
    public Text score;
    public GameObject pauseScreen;

    public static float maxDistanceTravelled;
    public static bool isPaused;

    float currentPlatformY;

    Pooler pooler;
    GameObject player;

    float worldScreenHeight, worldScreenWidth;
    float platformWidth;

    void Start () {
        pooler = Pooler.Instance;

        worldScreenHeight = Camera.main.orthographicSize * 2f;
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        platformWidth = platformPrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        SpawnPlayer();

        maxDistanceTravelled = 0f;

        isPaused = false;
    }

    void Update() {
        if (player != null) {
            float top = GameObject.FindGameObjectWithTag("MainCamera").transform.position.y +
                worldScreenHeight * 3 / 2;

            float minX = -worldScreenWidth / 2 + platformWidth / 2;
            float maxX = worldScreenWidth / 2 - platformWidth / 2;
            float y = Mathf.Pow(player.GetComponent<PlayerController>().bounceForce, 2) /
                (-2 * Physics2D.gravity.y * player.GetComponent<Rigidbody2D>().gravityScale) - 1;
            //y = Random.Range(y - 2, y);

            if (currentPlatformY + y < top) {

                Collider2D[] colliders;
                float spawnX, spawnY;
                
                spawnX = Random.Range(minX, maxX);
                spawnY = currentPlatformY + y;

                SpawnPlatform(new Vector2(spawnX, spawnY));
                

                if (Random.value < 0.5f) {
                    do {
                        spawnX = Random.Range(minX, maxX);
                        spawnY = currentPlatformY + Random.Range(y - 1, y);

                        CapsuleCollider2D cap = platformPrefab.GetComponent<CapsuleCollider2D>();

                        colliders = Physics2D.OverlapCapsuleAll(new Vector2(spawnX, spawnY), cap.size, cap.direction, 0);
                    } while (colliders.Length != 0);

                    SpawnBrokenPlatform(new Vector2(Random.Range(minX, maxX),
                        currentPlatformY + Random.Range(spawnY - 1, spawnY + 1)));
                }

                currentPlatformY += y;
            }
        }

        maxDistanceTravelled = Mathf.Max(maxDistanceTravelled,
            player.transform.position.y);
        score.text = "Score: " + (maxDistanceTravelled * 10).ToString("F0");

        if (Input.GetKeyUp(KeyCode.Escape)) {            
            if (isPaused) {
                Unpause();
            } else {
                Pause();
            }
        }
    }

    void SpawnPlayer() {
        player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);

        SpawnPlatform(spawnPos - new Vector2(0, platformOffset));

        currentPlatformY = spawnPos.y - platformOffset;
    }

    public void SpawnPlatform(Vector2 pos) {
        pooler.spawn("Platform", pos);
    }

    public void SpawnBrokenPlatform(Vector2 pos) {
        pooler.spawn("Broken Platform", pos);
    }

    public void Pause() {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Unpause() {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }
}
