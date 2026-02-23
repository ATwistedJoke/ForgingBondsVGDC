using UnityEngine;
using FMODUnity;
using System.Collections.Generic;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance {get; private set;}

    private List<EventInstance> eventInstances; 

    private EventInstance musicEventInstance; 

    private void Start()
    {
        InitializeMusic(FMODEvents.instance.music);
    }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one Audio Manager in scene"); 
        }
        instance = this; 

        eventInstances = new List<EventInstance>(); 
    }
    
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateGameInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference); 
        eventInstances.Add(eventInstance); 
        return eventInstance;
    }

    public void SetMusicArea(float value)
    {
        musicEventInstance.setParameterByName("CurrentTheme", value);
    }

    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateGameInstance(musicEventReference);
        musicEventInstance.start(); 
    }

    private void Cleanup()
    {
        foreach(EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); 
        } 
    }

    private void OnDestroy()
    {
        Cleanup(); 
    }
}
