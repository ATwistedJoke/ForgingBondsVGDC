using UnityEngine;

public class PickUpIron : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool pickedUp = false;
    public GameObject tongs;
    private Transform tongsTransform;
    private Vector3 posInTongs;
    public SpriteRenderer spriteRenderer;
    public Sprite openTongs;
    public Sprite closedTongs;
    void Start()
    {
        tongs = GameObject.FindGameObjectWithTag("Tongs");
        spriteRenderer = tongs.GetComponent<SpriteRenderer>();
        tongsTransform = tongs.transform;
        posInTongs = new Vector3(0.16f, 8.43f, 0);
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tongs"))
        {
            if(!pickedUp)
            {
                transform.SetParent(tongsTransform, true);
                transform.localPosition = posInTongs;
                pickedUp = true;
                spriteRenderer.sprite = openTongs;
            }
        }
    }
    public void setDown()
    {
        if (pickedUp)
        {
            transform.SetParent(null, true);
            pickedUp = false;
            spriteRenderer.sprite = closedTongs;
        }
    }
}
