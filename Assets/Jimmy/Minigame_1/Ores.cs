using Unity.VisualScripting;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.EventSystems;


//functionality for shattering and breaking ore
public class Ores : MonoBehaviour, IPointerClickHandler
{
    public GameController game;
    [SerializeField] private int required_break;
    int current_break = 0;
    public enum OreType { Iron, Gold, Mythril }
    public enum Ore_State { Untouched, Mining, Shatter }
    [SerializeField] public OreType oreType;


    //handle clicks,
    //to do: integrate clicking sound effect to imitate mining
    //ores will enter "mining state" - cracks increasingly show, 
    //if enough clicks break
    public void Handle_Click()
    {
        Debug.Log("registering click");

        current_break++;
        Debug.Log(current_break);
        if (current_break >= required_break)
        {
            Break();
        }

    }

    //called when ores are clicked enough
    //based on ore type, give increasing points on rarity
    public void Break()
    {

        Destroy(gameObject);
        switch (oreType)
        {
            case OreType.Iron:
                game.AddScore(10);
                Debug.Log("+10 points to you!");
                break;

            case OreType.Gold:
                game.AddScore(25);
                Debug.Log("+25 points to you!");
                break;

            case OreType.Mythril:
                game.AddScore(100);
                Debug.Log("+100 points to you!");
                break;

        }

        Debug.Log("you probably should've got points rn");

    }

    //this is the way to handle clicks in unity ig
    public void OnPointerClick(PointerEventData eventData)
    {   
        //check if ore is being clicked on 
         Ores ore = eventData.pointerPress.GetComponent<Ores>();
             if (ore != null)
             {
                 ore.Handle_Click();
             }
    }
}
