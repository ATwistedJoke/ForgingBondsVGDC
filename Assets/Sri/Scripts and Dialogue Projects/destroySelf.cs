using System.Collections;
using UnityEngine;

public class destroySelf : MonoBehaviour
{

    public float destroyDelay = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
