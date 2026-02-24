using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneSwitchingDummyScript : MonoBehaviour
{
    public void GoToScene()
    {
        SceneManager.LoadScene("DialogueCreation");
    }
}
