using UnityEngine;
using System.Collections;


public class DragObject : MonoBehaviour 
{

    private Vector3 screenPoint;
    private Vector3 offset;
    public GameObject[] snapPositions;
    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        snapPositions = GameObject.FindGameObjectsWithTag("SnapPoint");
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
        transform.position = findClosestSnapPoint().transform.position;
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
        foreach(GameObject screenPoint in snapPositions)
        {
            if (screenPoint.transform != null)
            {
                // Calculate the distance between this object and the target object
                float distance = Vector3.Distance(transform.position, screenPoint.transform.position);
                if(distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = screenPoint;
                }
                // Log the distance to the console (for debugging)
            }
        }
        if(closestDistance >= 1.5f)
        {
            return gameObject;
        }
        return closest;
    }
}