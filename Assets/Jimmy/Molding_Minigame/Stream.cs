using UnityEngine;

public class Stream : MonoBehaviour
{
    public LineRenderer stream;
    public RectTransform tipping_point;
    public GameObject crucible;
    public Mold mold;
    //public ParticleSystem splashParticles; // Add a particle system here!

    [SerializeField] private float maxRange = 1000f; // UI distances are larger
    [SerializeField] private float minPourAngle = 45f;
    [SerializeField] private float streamSpeed = 500f; // Speed liquid falls

    private bool isPouring;

    void Update()
    {
        bool shouldPour = CalculatePourAngle() > minPourAngle;

        if (shouldPour && !isPouring)
        {
            StartPour();
        }
        else if (!shouldPour && isPouring)
        {
            EndPour();
        }

        if (isPouring)
        {
            UpdateStreamPositions();
        }
    }

    void StartPour()
    {
        isPouring = true;
        stream.enabled = true;
        stream.positionCount = 2;
        // Start both points at the tip so it "grows" out
        stream.SetPosition(0, tipping_point.position);
        stream.SetPosition(1, tipping_point.position);
    }

    void EndPour()
    {
        isPouring = false;
        stream.enabled = false;
        //if (splashParticles != null) splashParticles.Stop();
    }

    void UpdateStreamPositions()
    {
        // 1. The start is always attached to the jar
        stream.SetPosition(0, tipping_point.position);

        // 2. Raycast to find where the liquid SHOULD be hitting
        RaycastHit2D hit = Physics2D.Raycast(tipping_point.position, Vector2.down, maxRange);
        Vector3 targetPos = (hit.collider != null) ? (Vector3)hit.point : tipping_point.position + Vector3.down * maxRange;

        // 3. Animate the end point toward the target (Logic from the video)
        Vector3 currentEndPos = stream.GetPosition(1);
        Vector3 newEndPos = Vector3.MoveTowards(currentEndPos, targetPos, streamSpeed * Time.deltaTime);
        stream.SetPosition(1, newEndPos);

        // 4. Handle Splash & Filling (Only if the stream has reached the target)
        float distanceToTarget = Vector3.Distance(newEndPos, targetPos);
        if (distanceToTarget < 0.1f && hit.collider != null)
        {
            //if (splashParticles != null && !splashParticles.isPlaying) splashParticles.Play();
            //if (splashParticles != null) splashParticles.transform.position = hit.point;

            if (hit.collider.CompareTag("Mold"))
            {
                Debug.Log("hitting mold");
                mold.Fill(Time.deltaTime /2);
            }
        }
        else
        {

            Debug.Log("I am hitting nothing");
            //if (splashParticles != null) splashParticles.Stop();
        }
    }

    private float CalculatePourAngle()
    {
        float angle = crucible.transform.eulerAngles.z;
        if (angle > 180) {angle = 360 - angle;}
        Debug.Log(angle);
        return angle;
    }
}
