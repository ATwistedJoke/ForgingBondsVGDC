using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Stream : MonoBehaviour
{
    public LineRenderer stream;
    public RectTransform tipping_point;
    public GameObject crucible;
    public Mold mold;

    public float offsetx;
    public float offsety;

    public Molding_Minigame game;
    public ParticleSystem splashParticles; // Add optional particle system here

    [SerializeField] private float maxRange = 250f; // UI distances are larger
    [SerializeField] private float minPourAngle = 45f;
    [SerializeField] private float streamSpeed = 500f; // Speed liquid falls
    [SerializeField] private float fillspeed = 10f; //how fast liquid fills mold
    [SerializeField] private float streamZ = -1f; // Adjust this to show in front of UI
    public float perfect_pour_angle;

    public bool isPouring;

    void Update()
    {

        bool angle_correct = CalculatePourAngle() > minPourAngle;
        bool ore_threshold = game.totalOreCount >=3; 

        bool should_pour = angle_correct && ore_threshold;

        if (should_pour && !isPouring)
        {
            StartPour();
        }
        else if (!should_pour && isPouring)
        {
            Debug.Log("stop man");
            EndPour();
            splashParticles.Stop();
        }

        if (isPouring)
        {
             UpdateStreamPositions();
        }
    }

    void StartPour()
    {
        Debug.Log("START POUR");
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
        stream.positionCount = 0;
        stream.enabled = false;
        Debug.Log("stream should be disabled");
        if (splashParticles != null) splashParticles.Stop();
    }

 void UpdateStreamPositions()
    {
        // Get the starting position from the UI tipping point
        Vector3 startPos = GetWorldTippingPoint();
        stream.SetPosition(0, startPos);

        //Perform Raycast on Z = 0 (where Physics2D lives)
        Vector2 rayOrigin = new Vector2(startPos.x, startPos.y);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, maxRange);
        
        //Determine where the liquid SHOULD hit
        Vector3 targetPos;
        if (hit.collider != null)
        {
            targetPos = new Vector3(hit.point.x, hit.point.y, streamZ);
        }
        else
        {
            targetPos = startPos + (Vector3.down * maxRange);
        }

        //Animate the end point falling down
        Vector3 currentEndPos = stream.GetPosition(1);
        Vector3 newEndPos = Vector3.MoveTowards(currentEndPos, targetPos, streamSpeed * Time.deltaTime);
        newEndPos.z = streamZ; // Lock Z to keep the line vertical
        stream.SetPosition(1, newEndPos);

        //Handle Collision Logic (Splash & Filling)
        float distanceToTarget = Vector3.Distance(newEndPos, targetPos);
        if (distanceToTarget < 0.1f && hit.collider != null)
        {
            if (splashParticles != null)
            {
                if (!splashParticles.isPlaying) splashParticles.Play();
                splashParticles.transform.position = new Vector3(hit.point.x, hit.point.y, streamZ);
            }

            if (hit.collider.CompareTag("Mold"))
            {
                Mold hitMold = hit.collider.GetComponent<Mold>();
                if (hitMold != null)
                {
                    if(CalculatePourAngle() == perfect_pour_angle)
                    {
                        fillspeed+=10; //bonus if perfect angle
                    }
                    hitMold.Fill(Time.deltaTime / fillspeed);
                }
            }
        }
        else
        {
            if (splashParticles != null) splashParticles.Stop();
        }
    }

    // Helper to get the correct world position of the UI tipping point
    private Vector3 GetWorldTippingPoint()
    {
        Vector3 pos = tipping_point.position;
        pos.x += offsetx;
        pos.y += offsety;
        pos.z += streamZ;
        return pos;}
    
    private float CalculatePourAngle()
    {
        float angle = crucible.transform.eulerAngles.z;
        if (angle > 180) {angle = 360 - angle;}
        Debug.Log(angle);
        return angle;
    }
}
