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
    public GameObject enemyPrefab;

    [Space]

    [Header("Spawn rate")]
    [Range(0f, 1f)] public float brokenPlatformRate;
    [Range(0f, 1f)] public float enemyRate;

    [Space]

    [Header("Others")]
    public Text score;
    public GameObject pauseScreen;
    public GameObject instructionScreen;
    public float minSpawnGap;
    public float maxSpawnGap;


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

        if (PlayerPrefs.GetInt("Played", 0) == 0) {
            ShowInstruction();
        }

        PlayerPrefs.SetInt("Played", PlayerPrefs.GetInt("Played", 0) + 1);

        isPaused = false;        
    }

    void Update() {
        if (player != null) {
            float top = GameObject.FindGameObjectWithTag("MainCamera").transform.position.y +
                worldScreenHeight * 3 / 2;

            float minX = -worldScreenWidth / 2 + platformWidth / 2;
            float maxX = worldScreenWidth / 2 - platformWidth / 2;
            float y = Random.Range(minSpawnGap, maxSpawnGap);
            //y = Random.Range(y - 2, y);

            if (currentPlatformY + y < top) {

                //Collider2D[] colliders;
                float spawnX, spawnY;
                
                spawnX = Random.Range(minX, maxX);
                spawnY = currentPlatformY + y;

                SpawnPlatform(new Vector2(spawnX, spawnY));
                
                // broken platform
                if (Random.value < brokenPlatformRate) {
                    /*
                    do {
                        spawnX = Random.Range(minX, maxX);
                        spawnY = currentPlatformY + Random.Range(y - 1, y);

                        CapsuleCollider2D cap = brokenPlatformPrefab.GetComponent<CapsuleCollider2D>();

                        colliders = Physics2D.OverlapCapsuleAll(new Vector2(spawnX, spawnY), cap.size, cap.direction, 0);
                    } while (colliders.Length != 0);
                    
                    SpawnBrokenPlatform(new Vector2(Random.Range(minX, maxX),
                        currentPlatformY + Random.Range(spawnY - 1, spawnY + 1)));
                    */

                    spawnX = Random.Range(minX, maxX);
                    spawnY = currentPlatformY + Random.Range(y - 1, y);

                    SpawnBrokenPlatform(new Vector2(spawnX, spawnY));
                }

                // enemy
                if (Random.value < enemyRate) {
                    /*
                    do {
                        spawnX = Random.Range(minX, maxX);
                        spawnY = currentPlatformY + Random.Range(y - 1, y);

                        CircleCollider2D circle = enemyPrefab.GetComponent<CircleCollider2D>();

                        colliders = Physics2D.OverlapCircleAll(new Vector2(spawnX, spawnY), circle.radius, 0);
                    } while (colliders.Length != 0);

                    SpawnEnemy(new Vector2(Random.Range(minX, maxX),
                        currentPlatformY + Random.Range(spawnY - 1, spawnY + 1)));
                    */

                    spawnX = Random.Range(minX, maxX);
                    spawnY = currentPlatformY + Random.Range(y - 1, y);

                    SpawnEnemy(new Vector2(spawnX, spawnY));
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
        //SpawnBrokenPlatform(spawnPos + new Vector2(0, platformOffset));

        currentPlatformY = spawnPos.y - platformOffset;
    }

    void ShowInstruction() {
        instructionScreen.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void HideInstruction() {
        instructionScreen.SetActive(false);
    }

    public void SpawnPlatform(Vector2 pos) {
        pooler.spawn("Platform", pos);
    }

    public void SpawnBrokenPlatform(Vector2 pos) {
        pooler.spawn("Broken Platform", pos);
    }

    void SpawnEnemy(Vector2 pos) {
        pooler.spawn("Enemy", pos);
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
