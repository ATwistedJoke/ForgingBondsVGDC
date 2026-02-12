using System.Collections;
using TMPro;
using UnityEngine;

public class TimerJ : MonoBehaviour
{
    public TextMeshProUGUI textbox;

    public bool TimerOn = false;

    public float time = 180f;

    public Molding_Minigame gameManager;

    void Start()
    {
        TurnTimerOn();
        Debug.Log(TimerOn);
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (TimerOn)
        {
            time -= Time.deltaTime;

              // Update timer display
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            textbox.text = string.Format("{0:00} : {1:00}", minutes, seconds);


            // Clamp time to zero if it goes below
            if (time <= 0)
            {
                time = 0;
                TimerOn = false;

                // Trigger end condition
                gameManager.End_Minigame();
                yield break; // Exit the coroutine
            }
            yield return null;
        }

    }
    public void TurnTimerOn()
    {
        TimerOn = true;
    }

    }