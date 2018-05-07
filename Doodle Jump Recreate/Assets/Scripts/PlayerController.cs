using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour {

    public int horizontalSpeed;

    [Range(1f, 5f)] public float fallMultiplier;

    Rigidbody2D rb;
    SpriteRenderer sr;

    private float horizontalMovement;
    private bool jumpRequest;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update() {
        horizontalMovement = Input.GetAxis("Horizontal");

        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        float height = Camera.main.orthographicSize;
        float worldScreenWidth = height * 2f / Screen.height * Screen.width;

        if (transform.position.x < -worldScreenWidth / 2) {
            transform.Translate(worldScreenWidth, 0, 0);
        }

        if (transform.position.x > worldScreenWidth / 2) {
            transform.Translate(-worldScreenWidth, 0, 0);
        }

        if (transform.position.y + sr.bounds.size.y < cam.transform.position.y - height) {
            Gameover();
        }
    }

    void FixedUpdate() {
        transform.Translate(horizontalMovement * horizontalSpeed * Time.deltaTime, 0, 0);

        if (rb.velocity.y < 0) {
            rb.gravityScale = fallMultiplier;
        } else {
            rb.gravityScale = 1f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Broken Platform") {
            if (collision.relativeVelocity.y <= 0) {
                collision.gameObject.SetActive(false);
            }
        }

        if (collision.gameObject.tag == "Enemy") {
            Gameover();
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
