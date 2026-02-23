using UnityEngine;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float currCountdownValue;
    public float startCountdownValue = 60f;
    public TMP_Text timerText;
    public GameObject minigamePrefab;
    void Awake()
    {
        timerText = GetComponent<TMP_Text>();
        timerText.text = "" + startCountdownValue;
        StartCoroutine(StartCountdown());
    }

    // Update is called once per frame
    public IEnumerator StartCountdown()
    {
        currCountdownValue = startCountdownValue;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
            timerText.text = "" + currCountdownValue;
        }
        minigamePrefab = GameObject.FindGameObjectWithTag("minigame");
        Destroy(minigamePrefab);
        Destroy(gameObject);
    }
}
