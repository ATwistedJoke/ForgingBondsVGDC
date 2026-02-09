using System;
using System.Collections.Generic;

public class AudioModel
{
    // Event for listeners (like AudioController) to react to volume changes
    public event Action OnVolumeChanged;

    // Registry of sounds (in Unity you'd probably use AudioSource instead)
    public Dictionary<string, UnityEngine.AudioSource> Sounds { get; } 
        = new Dictionary<string, UnityEngine.AudioSource>();

    private float _bgmVolume;
    private float _sfxVolume;

    public float BgmVolume
    {
        get => _bgmVolume;
        private set
        {
            _bgmVolume = ClampVolume(value);
            OnVolumeChanged?.Invoke();
        }
    }

    public float SfxVolume
    {
        get => _sfxVolume;
        private set
        {
            _sfxVolume = ClampVolume(value);
            OnVolumeChanged?.Invoke();
        }
    }

    public AudioModel()
    {
        _bgmVolume = GameConstants.DEFAULT_BGM_VOLUME;
        _sfxVolume = GameConstants.DEFAULT_SFX_VOLUME;
    }

    public void SetBgmVolume(float volume)
    {
        BgmVolume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        SfxVolume = volume;
    }

    private float ClampVolume(float v)
    {
        if (v < GameConstants.VOLUME_MIN) return GameConstants.VOLUME_MIN;
        if (v > GameConstants.VOLUME_MAX) return GameConstants.VOLUME_MAX;
        return v;
    }
}
