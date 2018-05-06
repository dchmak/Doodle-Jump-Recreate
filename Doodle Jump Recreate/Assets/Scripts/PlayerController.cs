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
            Debug.Log("Gameover!");
            AudioController audioCtrl = FindObjectOfType<AudioController>();
            audioCtrl.stop();
            audioCtrl.play("PlayerDeath");
            Destroy(gameObject);

            SceneManager.LoadScene("Gameover");
        }
    }

    void OnBecameInvisible() {
        //Debug.Log("Out of screen!");
    }

    void OnCollisionEnter2D (Collision2D collision) {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (sr == null) sr = GetComponent <SpriteRenderer>();

        //Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.tag == "Platform") {
            float playerPosY = transform.position.y - sr.bounds.size.y / 2;
            float platformPosY = collision.transform.position.y;

            if (playerPosY >= platformPosY) {
                rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            }
        }
    }
}
