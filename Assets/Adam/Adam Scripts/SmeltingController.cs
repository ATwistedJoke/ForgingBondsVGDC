using UnityEngine;

public class SmeltingController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject blackBar; //Player-controlled bar
    public float barLeftSpeed = 500f; //Speed of bar when untouched (goes left)
    public float barRightSpeed = 500f; //Speed of bar when player holds space (goes right)
    public int score = 0;
    private bool addScore = false;
    public GameObject barContainer; //Contains large bar
    private Vector3 leftEdgePosition_World; //Vector3 of left boundary of large bar
    private Vector3 rightEdgePosition_World; 
    private float leftEdgeX_World; //Float position of left bounary of bar
    private float rightEdgeX_World;
    void Start()
    {
        RectTransform barContainerTransform = barContainer.GetComponent<RectTransform>();

        leftEdgeX_World = barContainerTransform.position.x + barContainerTransform.rect.xMin;
        rightEdgeX_World = barContainerTransform.position.x + barContainerTransform.rect.xMax;

        leftEdgePosition_World = new Vector3(leftEdgeX_World, barContainerTransform.position.y, barContainerTransform.position.z);
        rightEdgePosition_World = new Vector3(rightEdgeX_World, barContainerTransform.position.y, barContainerTransform.position.z);
    }

    // Update is called once per frame
    void Update() {
        //Move right if space is held and black bar would stay in boundary
        if (Input.GetKey(KeyCode.Space) && blackBar.transform.position.x < rightEdgeX_World)
        {
            blackBar.transform.Translate(Vector3.right * barRightSpeed * Time.deltaTime);
        }
        else if(blackBar.transform.position.x > leftEdgeX_World)
        {
            blackBar.transform.Translate(Vector3.left * barLeftSpeed * Time.deltaTime);
        }
        if (addScore)
        {
            score++;
        }
        barLeftSpeed += 0.01f; //Bar speeds up over time
    }
    private void OnTriggerEnter2D(Collider2D other) { //Checks when black bar enters scoring area
        addScore = true;
    }
    private void OnTriggerExit2D(Collider2D other) {
        addScore = false;
    }
}
