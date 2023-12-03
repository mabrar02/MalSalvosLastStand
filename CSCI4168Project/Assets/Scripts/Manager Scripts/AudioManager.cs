using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;
    public Sound[] sounds;
    private bool shopOpen;

    private void Awake() {
        Instance = this;
        GameManager.OnGameStateChanged += GameManagerStateChange;
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
        shopOpen = false;
    }

    private void GameManagerStateChange(GameState state) {
        if (state == GameState.BuildPhase) {
            Stop("BattleTheme");
            Play("BuildTheme");
        }
        else if (state == GameState.SpawnPhase) {
            Stop("BuildTheme");
            Play("BattleTheme");
        }
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null || s.source == null) // Check if s or its source is null
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null || s.source == null) // Check if s or its source is null
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void ShopSound() {
        if (shopOpen) {
            Play("ShopClose");
        }
        else {
            Play("ShopOpen");
        }
        shopOpen = !shopOpen;
    }
}
