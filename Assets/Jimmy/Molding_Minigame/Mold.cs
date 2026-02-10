using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Mold : MonoBehaviour
{

    public Image fill_image;

    public bool filled;
    public Molding_Minigame game;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fill_image = fill_image.GetComponent<Image>();

        fill_image.fillAmount = 0;


    }
    public void Fill(float fill_Amount)
    {
        if(filled){return;}
        fill_image.fillAmount += fill_Amount;
        
        if(fill_image.fillAmount >= 1)
        {
            Debug.Log("filled!");
            filled = true;
            ReplaceMold();
        }

    }
    private void ReplaceMold()
    {
        
        if (game != null)
        {
            Debug.Log("coroutine started");
            game.StartCoroutine(game.OnMoldFilled(this));
        }
        Debug.Log("must delete mold");
        // Self-destruct so the new one can take its place in the UI
        Destroy(this.gameObject);
        filled = false;
    }
}
