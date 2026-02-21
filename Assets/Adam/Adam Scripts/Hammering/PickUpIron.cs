using UnityEngine;

public class PickUpIron : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool pickedUp = false;
    public GameObject tongs;
    private Transform tongsTransform;
    private Vector3 posInTongs;
    void Start()
    {
        tongs = GameObject.FindGameObjectWithTag("Tongs");
        tongsTransform = tongs.transform;
        posInTongs = new Vector3(0f, 5f, 0);
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
            }
        }
    }
    public void setDown()
    {
        if (pickedUp)
        {
            transform.SetParent(null, true);
            pickedUp = false;
        }
    }
}
