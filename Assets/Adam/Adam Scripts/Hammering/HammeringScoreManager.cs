using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class HammeringScoreManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int score;
    public GameObject ingot;
    public HeatIngot heatIngot;
    public float maxScore = 100f;
    public Sprite level1;
    public Sprite level2;
    public Sprite level3;
    public Sprite level4;
    public float milestone1 = 100f;
    public float milestone2 = 250f;
    public float milestone3 = 500f;
    private SpriteRenderer ingotRenderer;

    void Start()
    {
        ingot = GameObject.FindGameObjectWithTag("Ingot");
        ingotRenderer = ingot.GetComponent<SpriteRenderer>();
        heatIngot = ingot.GetComponent<HeatIngot>();
        ingotRenderer.sprite = level1;
    }

    // Update is called once per frame
    public void addScore(int increment)
    {
        int newScore = score + heatIngot.heatScore() * increment;
        score = newScore;
        if(score >= milestone3)
        {
            ingotRenderer.sprite = level4;
        }
        else if(score >= milestone2)
        {
            ingotRenderer.sprite = level3;
        }
        else if(score >= milestone1)
        {
            ingotRenderer.sprite = level2;
        }
    }

    public void OnDestroy()
    {
        GameManager.instance.GiveResult(CalculateResult(score));
    }

    public int CalculateResult(int finalScore)
    {
        if(finalScore >= milestone3){ return 2;}
        else if(finalScore >= milestone2){ return 1;}
        return 0; 
    }
}
