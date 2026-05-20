using UnityEngine;
using System.Collections;

public class TriggerSkillCheck : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject skillCheck;
    private bool ingotInPos = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hammer"))
        {
            if (ingotInPos)
            {
                skillCheck.SetActive(true);
            }
        }
        if (other.gameObject.CompareTag("Ingot"))
        {
            ingotInPos = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Hammer"))
        {
            if (ingotInPos)
            {
                skillCheck.GetComponentInChildren<RotateHammer>().hammerHitting = false;
                skillCheck.GetComponentInChildren<RotateHammer>().Hammer.transform.rotation = Quaternion.Euler(0,0,-45);
                skillCheck.SetActive(false);
            }
        }
        if (other.gameObject.CompareTag("Ingot"))
        {
            ingotInPos = false;
        }
    }
}
