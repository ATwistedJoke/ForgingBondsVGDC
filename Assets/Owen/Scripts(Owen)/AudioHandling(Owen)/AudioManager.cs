using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instnace {get; private set;}

    private void Awake()
    {
        if(instnace != null)
        {
            Debug.LogError("More than one Audio Manager in scene"); 
        }
        instnace = this; 
    }
    
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
}
