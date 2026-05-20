
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Crucible : MonoBehaviour
{
    //rotation settings
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxTilt = 90f;
    [SerializeField] private float minTilt = 0f;

    public Molding_Minigame game_manager;

    // DATA LIVES HERE NOW
    public Dictionary<Molding_Minigame.OreType, int> contents = new Dictionary<Molding_Minigame.OreType, int>()
    {
        { Molding_Minigame.OreType.Iron, 0 },
        { Molding_Minigame.OreType.Gold, 0 },
        { Molding_Minigame.OreType.Mythril, 0 }
    };

    public Sprite crucible_empty;
    public Sprite crucible_threshold;

    public int current_ore1;
    public int current_ore2;
    public int current_ore3;


    void Update()
    {
        HandleRotation();
        current_ore1 = contents[Molding_Minigame.OreType.Iron];
        current_ore2 = contents[Molding_Minigame.OreType.Gold];
        current_ore3 = contents[Molding_Minigame.OreType.Mythril];

        if(game_manager.totalOreCount >= 3){
            gameObject.GetComponent<Image>().sprite = crucible_threshold;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = crucible_empty;

        }
        
    }
    //adds ore of specific type
    public void AddOre(Molding_Minigame.OreType type)
    {
        contents[type]++;
        Debug.Log("New total: " + contents[type]);
        game_manager.totalOreCount++;
    }
    //restarts the current crucible contents
    //called when a mold is complete and a new recipe is drawn
    public void ClearContents()
    {
        contents[Molding_Minigame.OreType.Iron] = 0;
        contents[Molding_Minigame.OreType.Gold] = 0;
        contents[Molding_Minigame.OreType.Mythril] = 0;
        game_manager.totalOreCount = 0;
    }

    //triggered when ore is dropped into the crucible
    private void OnTriggerEnter2D(Collider2D collision)
{   
    if(collision.tag != null) {
        string tag = collision.tag;

        switch (tag)
        {
            case "Iron": 
            AddOre(Molding_Minigame.OreType.Iron);
            Destroy(collision.gameObject);
            break;

            case "Gold": 
            AddOre(Molding_Minigame.OreType.Gold);
            Destroy(collision.gameObject);
            break;

            case "Mythril": 
            AddOre(Molding_Minigame.OreType.Mythril);
            Destroy(collision.gameObject);
            break;


        }
        }

}

    //current logic for rotating the crucible
    //Use A to turn left
    //Use D to turn right
    private void HandleRotation()
    {
        float tiltInput = 0f;
        if (Input.GetKey(KeyCode.A)) tiltInput = 1f;
        if (Input.GetKey(KeyCode.D)) tiltInput = -1f;

        float currentZ = transform.localEulerAngles.z;
        if (currentZ > 180) currentZ -= 360;

        float newZ = Mathf.Clamp(currentZ + (tiltInput * rotationSpeed * Time.deltaTime), -maxTilt, minTilt);
        transform.localRotation = Quaternion.Euler(0, 0, newZ);
    }
}