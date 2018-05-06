using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    void Update() {
        //Debug.Log("Out of bound);
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        float height = Camera.main.orthographicSize;

        if (transform.position.y < cam.transform.position.y - height) { 
            gameObject.SetActive(false);
        }
    }
}
