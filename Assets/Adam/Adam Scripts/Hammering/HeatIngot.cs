using UnityEngine;
using System.Collections;

public class HeatIngot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float heatLevel = 0f;
    public float heatSpeed = 2f; //how many seconds to change heat level
    public float coolSpeed = 5f; //how many seconds it takes to cool down
    private float curHeatTime = 0f;
    private bool currentlyHeating = false;

    private SpriteRenderer spriteRenderer;

    public float heatMilestone1 = 2f;
    public float heatMilestone2 = 5f; //Used for ranges of heat levels
    public Sprite evolution1;
    public Sprite evolution2;
    public Sprite evolution3;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Fire"))
        {
            currentlyHeating = true;
            StartCoroutine(HeatingCooldown());
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Fire"))
        {
            currentlyHeating = false;
            StartCoroutine(CoolingCooldown());
        }
    }
    public IEnumerator HeatingCooldown() //just so that the heat level doesn't change every frame
    {
        curHeatTime = heatSpeed;
        while (curHeatTime > 0)
        {
            Debug.Log("Countdown: " + curHeatTime);
            yield return new WaitForSeconds(0.2f);
            curHeatTime -= 0.2f;
            if (!currentlyHeating)
            {
                yield break;
            }
        }
        heatLevel++;
        Debug.Log("Heat Level: " + heatLevel);
        changeSprite();
        StartCoroutine(HeatingCooldown());
    }

    public IEnumerator CoolingCooldown() //just so that the heat level doesn't change every frame
    {
        curHeatTime = coolSpeed;
        while (curHeatTime > 0)
        {
            Debug.Log("Countdown: " + curHeatTime);
            yield return new WaitForSeconds(0.2f);
            curHeatTime -= 0.2f;
            if (currentlyHeating)
            {
                yield break;
            }
        }
        heatLevel--;
        Debug.Log("Heat Level: " + heatLevel);
        changeSprite();
        StartCoroutine(CoolingCooldown());
    }

    void changeSprite()
    {
        if(heatLevel >= heatMilestone2)
        {
            spriteRenderer.sprite = evolution3;
        }
        else if(heatLevel >= heatMilestone1)
        {
            spriteRenderer.sprite = evolution2;
        }
        else
        {
            spriteRenderer.sprite = evolution1;
        }
    }
}
