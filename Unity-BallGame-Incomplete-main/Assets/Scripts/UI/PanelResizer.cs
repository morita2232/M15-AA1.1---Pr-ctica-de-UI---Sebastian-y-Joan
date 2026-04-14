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

    [Header("Width Limits")]
    public float minWidth = 100f;
    public float maxWidth = 600f;

    [Header("Spacing Limits")]
    public float minSpacing = 5f;
    public float maxSpacing = 50f;

    private Vector2 startMousePos;
    private float startWidth;

    void Awake()
    {
        // Finds BOTH horizontal AND vertical layout groups
        layoutGroups = panelTransform.GetComponentsInChildren<HorizontalOrVerticalLayoutGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startMousePos = eventData.position;
        startWidth = panelTransform.sizeDelta.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float deltaX = eventData.position.x - startMousePos.x;
        float newWidth = Mathf.Clamp(startWidth + deltaX, minWidth, maxWidth);

        // Resize panel
        panelTransform.sizeDelta = new Vector2(newWidth, panelTransform.sizeDelta.y);

        // Lock content
        contentTransform.anchoredPosition = new Vector2(0, contentTransform.anchoredPosition.y);

        float t = Mathf.InverseLerp(minWidth, maxWidth, newWidth);
        float spacing = Mathf.Lerp(minSpacing, maxSpacing, t);

        foreach (var group in layoutGroups)
        {
            group.spacing = spacing;
        }
    }

    public void ResetWidth(float width)
    {
        startWidth = width;

        foreach (var group in layoutGroups)
        {
            group.spacing = maxSpacing;
        }
    }
}