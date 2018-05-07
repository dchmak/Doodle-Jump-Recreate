using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsFunctions : MonoBehaviour {

	public void LoadScene(string name) {
        try {
            SceneManager.LoadScene(name);
        } catch (System.Exception e) {
            Debug.LogException(e, this);
        }
    }

    public void Quit() {
        Application.Quit();
    }

    public void PlayAudio(string name) {
        AudioController audioCtrl = FindObjectOfType<AudioController>();
        if (!audioCtrl.IsPlaying(name)) {
            audioCtrl.Play(name);
        }
    }

    public void ResetPlayerPref() {
        PlayerPrefs.DeleteAll();
    }
}
