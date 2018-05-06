using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsFunctions : MonoBehaviour {

	public void loadScene(string name) {
        try {
            SceneManager.LoadScene(name);
        } catch (System.Exception e) {
            Debug.LogException(e, this);
        }
    }

    public void quit() {
        Application.Quit();
    }

    public void playAudio(string name) {
        AudioController audioCtrl = FindObjectOfType<AudioController>();
        if (!audioCtrl.isPlaying(name)) {
            audioCtrl.play(name);
        }
    }
}
