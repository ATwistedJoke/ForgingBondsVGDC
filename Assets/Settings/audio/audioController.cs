using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioModel model;

    private bool hasUserInteracted = false;

    private void Awake()
    {
        if (model == null)
        {
            model = new AudioModel();
        }

        // Listen to volume changes like TS applyVolume()
        model.OnVolumeChanged += ApplyVolumeToAll;
    }

    private void Update()
    {
        // Rough equivalent of your DOM "keydown/mousedown/touchstart"
        if (!hasUserInteracted &&
            (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
        {
            hasUserInteracted = true;
        }
    }

    // REGISTER SOUND (like your TS registerSound)
    public void RegisterSound(string key, AudioSource source, bool overwrite = false)
    {
        if (!model.Sounds.ContainsKey(key) || overwrite)
        {
            model.Sounds[key] = source;

            // initial volume based on key
            source.volume = key.Contains("bgm")
                ? model.BgmVolume
                : model.SfxVolume;
        }
    }

    // PLAY
    public void Play(string key, bool loop = false)
    {
        if (!model.Sounds.TryGetValue(key, out var source)) return;

        source.loop = loop;

        if (!loop)
        {
            source.time = 0f; // reset like currentTime = 0
        }

        if (!hasUserInteracted) return;

        // Unity play is simpler, no browser AbortError stuff
        source.Play();
    }

    // STOP single sound
    public void Stop(string key)
    {
        if (!model.Sounds.TryGetValue(key, out var source)) return;

        source.Stop();
        source.time = 0f;
    }

    // STOP ALL
    public void StopAll()
    {
        foreach (var kvp in model.Sounds)
        {
            var source = kvp.Value;
            source.Stop();
            source.time = 0f;
        }
    }

    // VOLUME SETTERS (like your TS setBgmVolume / setSfxVolume)
    public void SetBgmVolume(float v)
    {
        model.SetBgmVolume(v);
    }

    public void SetSfxVolume(float v)
    {
        model.SetSfxVolume(v);
    }

    // This is like your TS applyVolume()
    private void ApplyVolumeToAll()
    {
        foreach (var kvp in model.Sounds)
        {
            string key = kvp.Key;
            AudioSource source = kvp.Value;

            source.volume = key.Contains("bgm")
                ? model.BgmVolume
                : model.SfxVolume;
        }
    }
}
