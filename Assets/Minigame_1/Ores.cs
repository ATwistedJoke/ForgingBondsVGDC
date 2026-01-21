using Unity.VisualScripting;
using UnityEngine;

//functionality for shattering and breaking ore
public class Ores : MonoBehaviour
{

    [SerializeField] private int required_break; 
    int current_break  = 0;
    public enum OreType { Iron, Gold, Mythril }
    public enum Ore_State {Untouched, Mining, Shatter}
    [SerializeField] public OreType oreType;
    

    void Update()
    {
        //get mouse input
    if (Input.GetMouseButtonDown(0))
    {   
        //track mouse position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //fire raycast from mouse
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            Ores ore = hit.collider.GetComponent<Ores>();
            if (ore != null)
            {
                ore.Handle_Click();
            }
        }
    }
}
    public void Handle_Click()
    {
        Debug.Log("registering click");
        current_break++; 
        if (current_break >= required_break)
        {
            Break();
        }
        
    }

    public void Break()
    {

        Destroy(gameObject);
        Debug.Log("you probably should've got points rn");
        


        
    }
}
