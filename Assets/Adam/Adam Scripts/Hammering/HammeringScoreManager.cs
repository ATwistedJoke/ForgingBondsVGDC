using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class HammeringScoreManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_Text scoreText;
    private int score;
    public GameObject ingot;
    public HeatIngot heatIngot;
    public Image scoreBar;
    public float maxScore = 100f;
    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "Score: 0/100";
        scoreBar = GameObject.FindGameObjectWithTag("ScoreBar").GetComponent<Image>();
        ingot = GameObject.FindGameObjectWithTag("Ingot");
        heatIngot = ingot.GetComponent<HeatIngot>();
    }

    // Update is called once per frame
    public void addScore(int increment)
    {
        int newScore = score + heatIngot.heatScore() * increment;
        score = newScore;
        scoreText.text = "Score: " + score + "/100";
        float fillPercentage = score / maxScore;
        scoreBar.fillAmount = fillPercentage;
    }
}
