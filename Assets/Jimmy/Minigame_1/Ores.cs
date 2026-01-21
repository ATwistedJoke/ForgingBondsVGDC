using Unity.VisualScripting;
using UnityEditor.Toolbars;
using UnityEngine;

//functionality for shattering and breaking ore
public class Ores : MonoBehaviour
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
}
