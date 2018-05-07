using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyController : MonoBehaviour {

    public Sprite[] sprites;

    private SpriteRenderer sr;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    private void Update () {
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        if (transform.position.y + sr.bounds.size.y / 2 < 
            cam.transform.position.y - Camera.main.orthographicSize) {
            gameObject.SetActive(false);
        }
    }
}
