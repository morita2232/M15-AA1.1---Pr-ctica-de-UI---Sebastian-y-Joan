using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelResizer : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [Header("Panel")]
    public RectTransform panelTransform;

    [Header("Content")]
    public RectTransform contentTransform;

    [Header("All Layout Groups")]
    private HorizontalOrVerticalLayoutGroup[] layoutGroups;

    [Header("Right Limits")]
    public float minRight = 100f;
    public float maxRight = 600f;

    [Header("Spacing Limits")]
    public float minSpacing = 5f;
    public float maxSpacing = 50f;

    private Vector2 startMousePos;
    private float startRight;

    void Awake()
    {
        layoutGroups = panelTransform.GetComponentsInChildren<HorizontalOrVerticalLayoutGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startMousePos = eventData.position;

        // Convert from Unity negative to usable positive value
        startRight = -panelTransform.offsetMax.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float deltaX = eventData.position.x - startMousePos.x;

        float newRight = Mathf.Clamp(startRight - deltaX, minRight, maxRight);

        // Apply RIGHT (convert back to negative)
        Vector2 offsetMax = panelTransform.offsetMax;
        offsetMax.x = -newRight;
        panelTransform.offsetMax = offsetMax;

        // Lock content position
        contentTransform.anchoredPosition =
            new Vector2(0, contentTransform.anchoredPosition.y);

        // Adjust spacing dynamically
        float t = Mathf.InverseLerp(minRight, maxRight, newRight);
        float spacing = Mathf.Lerp(minSpacing, maxSpacing, t);

        foreach (var group in layoutGroups)
        {
            group.spacing = spacing;
        }
    }

    public void ResetRight(float right)
    {
        startRight = right;

        foreach (var group in layoutGroups)
        {
            group.spacing = maxSpacing;
        }
    }
}