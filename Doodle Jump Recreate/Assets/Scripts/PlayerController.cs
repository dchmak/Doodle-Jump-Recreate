using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public int horizontalSpeed;
    public int bounceForce;

    Rigidbody2D rb;
    SpriteRenderer sr;

    void Update() {
        transform.Translate(Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime, 0, 0);

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
            // gameover
            AudioController audioCtrl = FindObjectOfType<AudioController>();
            audioCtrl.Stop();
            audioCtrl.Play("PlayerDeath");
            Destroy(gameObject);

            SceneManager.LoadScene("Gameover");
        }
    }

    void OnCollisionEnter2D (Collision2D collision) {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (sr == null) sr = GetComponent <SpriteRenderer>();
        
        float playerPosY = transform.position.y - sr.bounds.size.y / 2;
        float platformPosY = collision.transform.position.y;

        if (collision.gameObject.tag == "Platform") {
            if (playerPosY >= platformPosY) {
                rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            }
        }

        if (collision.gameObject.tag == "Broken Platform") {
            if (playerPosY >= platformPosY) {
                Destroy(collision.gameObject);
            }
        }
    }
}
