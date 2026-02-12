using UnityEngine;
using UnityEngine.UI;

public class AudioControllerSettings : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    private const string VolumeKey = "Volume";

    void Start()
    {
        Load();
        ChangeVolume();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        float v = PlayerPrefs.GetFloat(VolumeKey, 0.5f);
        volumeSlider.value = v;
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(VolumeKey, volumeSlider.value);
        PlayerPrefs.Save();
    }
}
