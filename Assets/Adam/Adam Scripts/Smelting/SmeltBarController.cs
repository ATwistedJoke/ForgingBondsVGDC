using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SmeltBarController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is createdp
    public LayoutElement leftBar;
    public LayoutElement rightBar;
    public LayoutElement greenBar;
    private bool goingRight = true; //direction green bar is going
    public float greenBarWidthDecrease = 1f; //how quickly the green bar shrinks
    public float greenBarMovementSpeed = 10f; //How quickly green bar is moving
    public BoxCollider2D greenBarCollider;
    public RectTransform greenBarRenderer;
    // Update is called once per frame
    void Awake()
    {
        StartCoroutine(PlayFireWoosh());
    }
    void FixedUpdate()
    {
        greenBar.flexibleWidth -= greenBarWidthDecrease;
        greenBarCollider.size = new Vector2(greenBarRenderer.rect.width, greenBarRenderer.rect.height);
        //Move green bar left and right by changing flexible widths of red bars
        if (goingRight)
        {
            rightBar.flexibleWidth -= greenBarMovementSpeed;
            leftBar.flexibleWidth += greenBarMovementSpeed;
            if(rightBar.flexibleWidth < 10)
            {
                goingRight = false;
            }
        }
        if (!goingRight)
        {
            leftBar.flexibleWidth -= greenBarMovementSpeed;
            rightBar.flexibleWidth += greenBarMovementSpeed;
            if(leftBar.flexibleWidth < 10)
            {
                goingRight = true;
            }
        }
    }
    public IEnumerator PlayFireWoosh() 
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.SFX[6], transform.position);
        yield return new WaitForSeconds(4.0f);
        StartCoroutine(PlayFireWoosh());
    }

}
