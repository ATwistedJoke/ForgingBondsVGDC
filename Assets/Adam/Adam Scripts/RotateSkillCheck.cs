using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkillCheck : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 180f;   
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite nextSprite;            


    private bool hitSkillCheck = false;

    void Update()
    {
        // Press SPACE to trigger the hit
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSkillCheckHit();
        }

        // Rotate **after** skill check was hit
        if (hitSkillCheck)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }

    private void OnSkillCheckHit()
    {
        if (hitSkillCheck) return; // prevent double triggering

        hitSkillCheck = true;

        // Swap sprite (if assigned)
        if (spriteRenderer != null && nextSprite != null)
        {
            spriteRenderer.sprite = nextSprite;
        }
    }
}
