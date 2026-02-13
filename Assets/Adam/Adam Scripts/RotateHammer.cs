using UnityEngine;

public class RotateHammer : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 180f;   // Degrees per second
    private bool insideHitbox = false;
    public GameObject skillCheck;
    public GameObject score;
    public HammeringScoreManager scoreManager;
    private int streak = 0;
    void Start()
    {
        skillCheck = GameObject.FindGameObjectWithTag("Hitbox");
        score = GameObject.FindGameObjectWithTag("Score");
        scoreManager = score.GetComponent<HammeringScoreManager>();
    }
    void OnEnable()
    {
        streak = 0;
    }
    private void Update() {
        // Rotate hammer continuously
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        // If hammer is overlapping hitbox AND player presses SPACE
        if (insideHitbox && Input.GetKeyDown(KeyCode.Space))
        {
            if(streak < 5)
            {
                streak++;
            }
            OnHammerHit();
        }
        else if(!insideHitbox && Input.GetKeyDown(KeyCode.Space))
        {
            streak = 0;
        }
    }

    private void OnHammerHit()
    {
        rotationSpeed *= -1;
        float randomAngle = Random.Range(0f, 360f);
        skillCheck.transform.rotation = transform.rotation = Quaternion.Euler(0f, 0f, randomAngle);
        scoreManager.addScore(streak);
        // TODO: your success logic here (play sound, change sprite, etc.)
    }

    // ------- 2D TRIGGERS ONLY (if using 2D colliders) -------
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
        {
            insideHitbox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
        {
            insideHitbox = false;
        }
    }
}
