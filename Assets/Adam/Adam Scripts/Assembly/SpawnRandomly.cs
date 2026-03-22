using UnityEngine;

public class SpawnRandomly : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject[] spawnPositions;
    void Start()
    {
        spawnPositions = GameObject.FindGameObjectsWithTag("SpawnPoint");
        int randomPos = Random.Range(0, spawnPositions.Length);
        GameObject spawnPos = spawnPositions[randomPos];
        SpriteRenderer spawnContainerRenderer = spawnPos.GetComponent<SpriteRenderer>();
        Bounds bounds = spawnContainerRenderer.bounds;

        Vector3 minPoint = bounds.min;
        Vector3 maxPoint = bounds.max;

        float minX = minPoint.x;
        float maxX = maxPoint.x;
        float minY = minPoint.y;
        float maxY = maxPoint.y;


        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        transform.position = new Vector3(randomX, randomY, 0);
    }
    
}
