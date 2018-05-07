using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BrokenPlatformController : MonoBehaviour {

    public GameObject brokenPlatformPiecesPrefab;
    [Range(0f, 10f)] public float torque;
    [Range(0f, 10f)] public float despawnTime;

    private void Update() {
        //Debug.Log("Out of bound);
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        float height = Camera.main.orthographicSize;

        if (transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y < cam.transform.position.y - height) {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Rigidbody2D>().velocity.y <= 0) {
            GameObject brokenPlatformPieces = Instantiate(brokenPlatformPiecesPrefab, transform.position, Quaternion.identity);
            Rigidbody2D[] childrenRB = brokenPlatformPieces.GetComponentsInChildren<Rigidbody2D>();

            foreach (Rigidbody2D childRB in childrenRB) {
                childRB.AddTorque(torque, ForceMode2D.Impulse);

                Destroy(childRB.gameObject, despawnTime);
            }

            gameObject.SetActive(false);
        }
    }
}
