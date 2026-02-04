using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Mold : MonoBehaviour
{

    public Image fill_image;

    public bool filled;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fill_image = fill_image.GetComponent<Image>();

        fill_image.fillAmount = 0;


    }
    public void Fill(float fill_Amount)
    {
        fill_image = fill_image.GetComponent<Image>();

        fill_image.fillAmount += fill_Amount;
        
        if(fill_image.fillAmount >= 100)
        {
            filled = true;
            

        }

        
    }
}
