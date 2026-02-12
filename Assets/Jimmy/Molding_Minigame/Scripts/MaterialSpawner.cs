using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MaterialSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject ironprefab;

    public GameObject goldprefab;

    public GameObject mythrilprefab;

    public RectTransform drop_point;

    [SerializeField] private GameObject spawn_mat;

    public void SpawnOre(string ore)
    {
        Debug.Log("i am being called");
        spawn_mat = null;

        switch (ore)
        {
            case "Iron":
                spawn_mat = ironprefab;
                break;
            case "Gold":
                spawn_mat = goldprefab;
                break;
            case "Mythril":
                spawn_mat = mythrilprefab;
                break;
        }

        if(spawn_mat != null)
        {
            GameObject spawned = Instantiate(spawn_mat, drop_point);
            Debug.Log(drop_point);
            RectTransform rt = spawned.GetComponent<RectTransform>();
            if (rt != null)
        {
            rt.anchoredPosition = Vector2.zero; // Snaps it to the parent's pivot
            rt.localScale = Vector3.one;       // Ensures it's not tiny or huge
        }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
