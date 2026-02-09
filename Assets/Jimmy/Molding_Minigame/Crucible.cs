
using UnityEngine;
using System.Collections.Generic;

public class Crucible : MonoBehaviour
{
    //rotation settings
    [SerializeField] private float rotationSpeed = 100f;
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

    public void AddOre(Molding_Minigame.OreType type)
    {
        contents[type]++;
    }

    public void ClearContents()
    {
        contents[Molding_Minigame.OreType.Iron] = 0;
        contents[Molding_Minigame.OreType.Gold] = 0;
        contents[Molding_Minigame.OreType.Mythril] = 0;
    }

    void Update()
    {
        HandleRotation();
    }

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