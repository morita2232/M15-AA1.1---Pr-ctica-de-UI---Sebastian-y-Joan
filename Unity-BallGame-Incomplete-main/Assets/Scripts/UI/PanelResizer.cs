using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelResizer : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [Header("Panel")]
    public RectTransform panelTransform;

    [Header("Content (fixed at X = 0)")]
    public RectTransform contentTransform;

    [Header("Layout Group")]
    public HorizontalLayoutGroup layoutGroup;

    [Header("Width Limits")]
    public float minWidth = 100f;
    public float maxWidth = 600f;

    [Header("Spacing Limits")]
    public float minSpacing = 5f;
    public float maxSpacing = 50f;

    private Vector2 startMousePos;
    private float startWidth;

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

        // Keep content locked at X = 0
        contentTransform.anchoredPosition = new Vector2(0, contentTransform.anchoredPosition.y);

        // Dynamic spacing
        float t = Mathf.InverseLerp(minWidth, maxWidth, newWidth);
        float spacing = Mathf.Lerp(minSpacing, maxSpacing, t);

        layoutGroup.spacing = spacing;
    }

    public void ResetWidth(float width)
    {
        startWidth = width;

        // Reset spacing too
        layoutGroup.spacing = maxSpacing;
    }
}