using UnityEngine;
using System.Collections;
using TMPro;

public class SmeltingTimer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float currCountdownValue;
    public float startCountdownValue = 60f;
    public TMP_Text timerText;
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
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
            timerText.text = "" + currCountdownValue;
        }

    }
}
