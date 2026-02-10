using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShieldSegment : MonoBehaviour, IPointerClickHandler
{
    [Header("Visual Settings")]
    [SerializeField] private Image segmentImage;
    [SerializeField] private Color normalColor = new Color(1f, 1f, 1f, 0.5f);
    [SerializeField] private Color hoverColor = new Color(1f, 1f, 0.8f, 0.7f);
    [SerializeField] private Color completedColor = new Color(0.5f, 1f, 0.5f, 0.9f);
    
    [Header("References")]
    [SerializeField] private ReinforcingMinigame minigameManager;
    

    private bool isCompleted = false;
    public bool IsCompleted => isCompleted;
    
    private void Start()
    {
        if (segmentImage == null)
            segmentImage = GetComponent<Image>();
        
        if (minigameManager == null)
            minigameManager = FindObjectOfType<ReinforcingMinigame>();
        
        ResetSegment();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isCompleted && minigameManager != null)
        {
            minigameManager.OnSegmentClicked(this);
        }
    }
    
    public void MarkCompleted()
    {
        isCompleted = true;
        if (segmentImage != null)
        {
            segmentImage.color = completedColor;
        }
    }
    
    public void ResetSegment()
    {
        isCompleted = false;
        if (segmentImage != null)
        {
            segmentImage.color = normalColor;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isCompleted && segmentImage != null)
        {
            segmentImage.color = hoverColor;
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isCompleted && segmentImage != null)
        {
            segmentImage.color = normalColor;
        }
    }
}