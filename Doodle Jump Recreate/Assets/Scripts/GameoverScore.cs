using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GameoverScore : MonoBehaviour {
	void Awake() {
        GetComponent<Text>().text = "Score: " + 
            (GameController.maxDistanceTravelled * 10).ToString("F0");
    }
}
