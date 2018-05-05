using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int horizontalSpeed;
    public int bounceForce;

    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

	void Update () {
        transform.Translate(Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime, 0, 0);
	}

    void OnCollisionEnter2D (Collision2D collision) {
        //Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.tag == "Platform") {
            rb.AddForce(Vector2.up * bounceForce);
        }
    }
}
