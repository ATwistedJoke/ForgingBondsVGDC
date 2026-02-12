using UnityEngine;

public class RotateHammer : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 180f;   // Degrees per second
    private bool insideHitbox = false;

    void Update()
    {
        // Rotate hammer continuously
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        // If hammer is overlapping hitbox AND player presses SPACE
        if (insideHitbox && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("HIT! Skill Check Success!");
            OnHammerHit();
        }
    }

    private void OnHammerHit()
    {
        // TODO: your success logic here (play sound, change sprite, etc.)
    }

    // ------- 2D TRIGGERS ONLY (if using 2D colliders) -------
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
            insideHitbox = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hitbox"))
            insideHitbox = false;
    }
}
