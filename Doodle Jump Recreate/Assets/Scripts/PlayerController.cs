using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour {

    public int horizontalSpeed;
    public int bounceForce;

    [Range(1f, 5f)] public float fallMultiplier;

    Rigidbody2D rb;
    SpriteRenderer sr;

    private bool jumpRequest;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update() {
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        float height = Camera.main.orthographicSize;
        float worldScreenWidth = height * 2f / Screen.height * Screen.width;

        if (transform.position.x < -worldScreenWidth / 2) {
            transform.Translate(worldScreenWidth, 0, 0);
        }

        if (transform.position.x > worldScreenWidth / 2) {
            transform.Translate(-worldScreenWidth, 0, 0);
        }

        if (transform.position.y < cam.transform.position.y - height) {
            Gameover();
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        float playerPosY = transform.position.y - sr.bounds.size.y / 2;
        float platformPosY = collision.transform.position.y;

        if (collision.gameObject.tag == "Platform") {
            if (playerPosY >= platformPosY && rb.velocity == Vector2.zero) {
                rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            }
        }

        if (collision.gameObject.tag == "Broken Platform") {
            if (playerPosY >= platformPosY && rb.velocity == Vector2.zero) {
                collision.gameObject.SetActive(false);
            }
        }

        if (collision.gameObject.tag == "Enemy") {
            Gameover();
        }
    }

    void FixedUpdate() {
        transform.Translate(Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime, 0, 0);

        if (rb.velocity.y < 0) {
            rb.gravityScale = fallMultiplier;
        } else {
            rb.gravityScale = 1f;
        }
    }  
    
    void Gameover() {
        AudioController audioCtrl = FindObjectOfType<AudioController>();
        audioCtrl.Stop();
        audioCtrl.Play("PlayerDeath");
        //Destroy(gameObject);

        SceneManager.LoadScene("Gameover");
    }
}
