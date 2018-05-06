﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {

    [System.Serializable]
    public class Sound {
        public string name;

        public AudioClip clip;
        public AudioMixerGroup mixer;

        [Range(0f, 1f)]
        public float volume;

        [Range(0.1f, 3f)]
        public float pitch;

        [HideInInspector]
        public AudioSource source;

        public bool loop;
    }

    public Sound[] sounds;

    public static AudioController instance;

	void Awake () {
        if (instance == null) instance = this;
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;
            sound.source.outputAudioMixerGroup = sound.mixer;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
	}

    void Start () {
        play("Background");
    }
	
	public void play (string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound " + name + " does not exist.");
            return;
        }
        s.source.Play();
    }

    public void stop () {
        foreach (Sound sound in sounds) {
            sound.source.Stop();
        }
    }

    public bool isPlaying(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound " + name + " does not exist.");
            return false;
        }
        return s.source.isPlaying;
    } 
}
