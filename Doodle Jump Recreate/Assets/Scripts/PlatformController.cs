using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    void OnBecameInvisible() {
        //Debug.Log("Out of bound!");
        gameObject.SetActive(false);
    }
}
