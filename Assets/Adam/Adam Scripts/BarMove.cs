using UnityEngine;

public class BarMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject blackBar;
    public float barLeft = 10f;
    public float barRight = 50f;
    public int score = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Space))
        {
            blackBar.transform.Translate(Vector3.right * barRight * Time.deltaTime);
        }
        else
        {
            blackBar.transform.Translate(Vector3.left * barLeft * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        score++;
        Debug.Log(score);
    }
}
