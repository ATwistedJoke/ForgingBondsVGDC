using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject itemBeingDragged;
    private Vector3 startPosition;
    //displacement of a vector value
    private Vector3 offset;
    public Canvas canvas;

    //panel for minigame 
    public Molding_Minigame gameManager;
    public CanvasGroup canvasGroup; 

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Debug.Log(itemBeingDragged.tag);
        // Debug.Log(itemBeingDragged.name);
        startPosition = transform.position;
        offset = transform.position - Input.mousePosition;
        transform.SetAsLastSibling(); // bring to front

        //initially, we allow for collision
        canvasGroup.blocksRaycasts = false;
        Debug.Log("please work");
    }


    public void OnDrag(PointerEventData eventData)
    {
        RectTransform rt = itemBeingDragged.GetComponent<RectTransform>();

        rt.anchoredPosition += eventData.delta;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; 
        //following the mouse cursor, project a raycast that hits with the crucible area
        GameObject hitObject = eventData.pointerCurrentRaycast.gameObject;
        Debug.Log(hitObject.name);

        if (hitObject != null && hitObject.CompareTag("Crucible"))
        {
            //switch statement for handling of different ingredients
            switch(itemBeingDragged.tag)
            {
                //case for all, use ingredient from basket, while also destroying game object
                case "Iron": 
                gameManager.AddOre(Molding_Minigame.OreType.Iron);
                break;

                case "Gold":
                gameManager.AddOre(Molding_Minigame.OreType.Iron);
                
                break;
                case "Milk":
                gameManager.AddOre(Molding_Minigame.OreType.Iron);
                break;
            }
        
            Debug.Log("Dropped in crucible!");
            Destroy(gameObject); // or SetActive(false), or snap into place
            return;
        }

        // Not dropped on crucible — return to original position
        transform.position = startPosition;
    }

    
}