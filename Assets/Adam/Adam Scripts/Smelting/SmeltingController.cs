using UnityEngine;
using System.Collections;
using TMPro;

public class SmeltingController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject blackBar; //Player-controlled bar
    public float barLeftSpeed = 500f; //Speed of bar when untouched (goes left)
    public float barRightSpeed = 500f; //Speed of bar when player holds space (goes right)
    public int score = 0;
    private bool addScore = false;
    public GameObject barContainer; //Contains large bar
    private float barContainerLeftEdgeX; //Float position of left bounary of bar
    private float barContainerRightEdgeX;
    [SerializeField] private TMP_Text scoreText;
    public float scoreTime = 1f;
    void Start()
    {
        scoreText.text = "Score: " + score;

        RectTransform barContainerTransform = barContainer.GetComponent<RectTransform>();
        
        Vector3[] corners = new Vector3[4];
        
        barContainerTransform.GetWorldCorners(corners);

        float globalWidth = Vector3.Distance(corners[0], corners[3]);
        float barContainerMidToEdge = globalWidth / 2.0f;
        barContainerLeftEdgeX = barContainerTransform.position.x - barContainerMidToEdge;
        barContainerRightEdgeX = barContainerTransform.position.x + barContainerMidToEdge;

    }

    // Update is called once per frame
    void Update() {
        Debug.Log(barContainer.transform.localPosition.x);
        //Move right if space is held and black bar would stay in boundary
        if (Input.GetKey(KeyCode.Space) && blackBar.transform.position.x < barContainerRightEdgeX)
        {
            blackBar.transform.localPosition += new Vector3(barRightSpeed * Time.deltaTime, 0, 0);
        }
        else if(blackBar.transform.position.x > barContainerLeftEdgeX)
        {
            blackBar.transform.localPosition -= new Vector3(barLeftSpeed * Time.deltaTime, 0, 0);
        }
        //barLeftSpeed -= 0.01f * Time.deltaTime; //Bar speeds up over time
    }
    private void OnTriggerEnter2D(Collider2D other) { //Checks when black bar enters scoring area
        addScore = true;
        StartCoroutine(StartCountdown());
    }
    private void OnTriggerExit2D(Collider2D other) {
        addScore = false;
    }
    //must be in green bar for a full second to gain score
    public IEnumerator StartCountdown(float scoreTimer = 1f) 
    {
        scoreTimer = scoreTime;
        while (scoreTimer > 0)
        {
            yield return new WaitForSeconds(0.01f);
            if (!addScore)
            {
                yield break;
            }
            scoreTimer -= 0.01f;
        }
        score++;
        scoreText.text = "Score: " + score;
        StartCoroutine(StartCountdown());
    }

    void OnDestroy()
    {
        int result = 0; 
        for(int i = 0; i < score; i+= 20)
        {
            result++; 
        }
        GameManager.instance.GiveResult(result);
    }
}
