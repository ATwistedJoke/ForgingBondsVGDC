using UnityEngine;
using System.Collections;
using FMODUnity;
public class CharacterManager : MonoBehaviour
{
    [SerializeField] public SpriteRenderer rnd; 
    [SerializeField] public Sprite[] list; 
    [SerializeField] public GameObject obj; 
    
    [field: Header("Voice Lines")]
    public EventReference[] VoiceLines;

    public int currSp; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rnd = GetComponent<SpriteRenderer>(); 
        currSp = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        rnd.sprite = list[currSp]; 
    }

    public void ChangeSprite(int i)
    {
        currSp = i; 
    }

    public void Move(int posX, int posY, int speed)
    {
        Vector3 target = new Vector3(posX,posY,0); 
        StartCoroutine(MoveOverTime(obj,target,speed));
    }

    private IEnumerator MoveOverTime(GameObject obj, Vector3 target, float spd)
    {
        while(obj != null && obj.transform.position != target)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, spd*Time.deltaTime); 
            yield return new WaitForEndOfFrame(); 
        }
    }

    public void PlayLine(int idx)
    {
        RuntimeManager.PlayOneShot(VoiceLines[idx + Random.Range(0,2)], transform.position);
    }

    void OnDestroy()
    {
        StopAllCoroutines(); 
    }
}
