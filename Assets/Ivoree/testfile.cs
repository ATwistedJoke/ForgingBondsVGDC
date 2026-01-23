using UnityEngine;
using Yarn.Unity;

public class testfile : MonoBehaviour
{
    // "climb" is the keyword we will use in the Yarn file
    [YarnCommand("climb")] 
    public static void CharacterClimb()
    {
        Debug.Log("The character is climbing!");
    }
}