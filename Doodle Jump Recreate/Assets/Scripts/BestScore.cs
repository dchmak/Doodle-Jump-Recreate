using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class BestScore : MonoBehaviour {
    void Awake() {
        if (GameController.maxDistanceTravelled * 10 > PlayerPrefs.GetFloat("Best Score", 0)) {
            PlayerPrefs.SetFloat("Best Score", GameController.maxDistanceTravelled * 10);
        }        

        GetComponent<Text>().text = "Best Score: " +
            PlayerPrefs.GetFloat("Best Score", 0).ToString("F0");
    }
}
