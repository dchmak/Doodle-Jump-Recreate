using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlatformController : MonoBehaviour {

    public int bounceForce;

    private void Update() {
        //Debug.Log("Out of bound);
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        float height = Camera.main.orthographicSize;

        if (transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y < cam.transform.position.y - height) { 
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null) {
                if (collision.relativeVelocity.y >= 0) {
                    rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
