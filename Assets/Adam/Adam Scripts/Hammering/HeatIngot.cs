using UnityEngine;
using System.Collections;

public class HeatIngot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float heatLevel = 0f;
    public float heatSpeed = 2f; //how many seconds to change heat level
    public float coolSpeed = 5f; //how many seconds it takes to cool down
    private float curHeatTime = 0f;
    public float heatMin = 0f;
    public float heatMax = 15f;
    private bool currentlyHeating = false;

    private SpriteRenderer spriteRenderer;

    public float heatMilestone1 = 2f;
    public float heatMilestone2 = 5f; //Used for ranges of heat levels
    public float heatMilestone3 = 12f;
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
            yield return new WaitForSeconds(0.2f);
            curHeatTime -= 0.2f;
            if (!currentlyHeating)
            {
                yield break;
            }
        }
        if(heatLevel < heatMax)
        {
            heatLevel++;
        }
        changeSprite();
        StartCoroutine(HeatingCooldown());
    }

    public IEnumerator CoolingCooldown() //just so that the heat level doesn't change every frame
    {
        curHeatTime = coolSpeed;
        while (curHeatTime > 0)
        {
            yield return new WaitForSeconds(0.2f);
            curHeatTime -= 0.2f;
            if (currentlyHeating)
            {
                yield break;
            }
        }
        if(heatLevel > heatMin)
        {
            heatLevel--;
        }
        changeSprite();
        StartCoroutine(CoolingCooldown());
    }

    void changeSprite()
    {
        if(heatLevel >= heatMilestone3)
        {
            spriteRenderer.color = Color.red;
        }
        else if(heatLevel >= heatMilestone2)
        {
            spriteRenderer.color = Color.orange;
        }
        else if(heatLevel >= heatMilestone1)
        {
            spriteRenderer.color = Color.yellow;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
    public int heatScore()
    {
        if(heatLevel >= heatMilestone3)
        {
            return -1;
        }
        else if(heatLevel >= heatMilestone2)
        {
            return 2;
        }
        else if(heatLevel >= heatMilestone1)
        {
            return 1;
        }
        return -1;
        
    }
}
