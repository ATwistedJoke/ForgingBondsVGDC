using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Sound Effects")]
    [field: SerializeField] public EventReference[] SFX {get; private set;}


    public static FMODEvents instnace { get; private set;}
    
    private void Awake()
    {
        if(instnace != null)
        {
            Debug.LogError("Found more than one FMOD Events instnace");
        }
        instnace = this; 
    }
}
