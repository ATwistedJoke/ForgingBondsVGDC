using UnityEngine;
using System.Collections;
using System;


public class DragObject : MonoBehaviour 
{

    private Vector3 screenPoint;
    private Vector3 offset;
    public GameObject[] snapPositions;
    public GameObject output;
    public CraftingOutput craftingOutput;
    private int pos = -1;
    public Vector3 startPos;

    void Start()
    {
        snapPositions = GameObject.FindGameObjectsWithTag("SnapPoint");
        output = GameObject.Find("Output");
        craftingOutput = output.GetComponent<CraftingOutput>();
        Array.Sort(snapPositions, CompareObNames);
        
        if(startPos == null)
        {
            startPos = gameObject.transform.position;
        }
    }
    void OnMouseDown()
    {

        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        if(pos != -1)
        {
            craftingOutput.removeItemFromTable(pos);
        }
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;

    }
    void OnMouseUp()
    {
        //go to empty parent's position in top right
        GameObject craftingSlot = findClosestSnapPoint();
        transform.position = craftingSlot.transform.position;
    }

    GameObject findClosestSnapPoint()
    {
        if(snapPositions.Length == 0)
        {
            return null;
        }
        if(snapPositions.Length == 1)
        {
            return snapPositions[0];
        }
        GameObject closest = snapPositions[0];
        float closestDistance = Vector3.Distance(transform.position, snapPositions[0].transform.position);
        pos = 0;
        for(int i = 0;i < snapPositions.Length; i++)
        {
            if (snapPositions[i].transform != null)
            {
                // Calculate the distance between this object and the target object
                float distance = Vector3.Distance(transform.position, snapPositions[i].transform.position);
                if(distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = snapPositions[i];
                    pos = i;
                }
                // Log the distance to the console (for debugging)
            }
        }
        if(closestDistance >= 1.5f)
        {
            return gameObject;
        }
        craftingOutput.addItemToTable(gameObject, pos);
        return closest;
    }

    public void returnToStartPos()
    {
        gameObject.transform.position = startPos;
    }

    int CompareObNames(GameObject x, GameObject y)
    {
        return x.name.CompareTo(y.name);
    }
    
}