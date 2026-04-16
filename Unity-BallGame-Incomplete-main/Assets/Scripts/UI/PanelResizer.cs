using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelResizer : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [Header("Panel")]
    public RectTransform panelTransform;

    [Header("Limits")]
    public float minWidth = 300f;
    public float maxWidth = 1000f;

    [Header("Spacing")]
    public float minSpacing = 5f;
    public float maxSpacing = 50f;

    private float startWidth;
    private Vector2 startMousePos;

    private HorizontalOrVerticalLayoutGroup[] layoutGroups;

    void Awake()
    {
        RefreshLayoutGroups();
    }

    void RefreshLayoutGroups()
    {
        layoutGroups = panelTransform.GetComponentsInChildren<HorizontalOrVerticalLayoutGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startMousePos = eventData.position;
        startWidth = panelTransform.sizeDelta.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float delta = eventData.position.x - startMousePos.x;

        float newWidth = Mathf.Clamp(startWidth - delta, minWidth, maxWidth);

        Vector2 size = panelTransform.sizeDelta;
        size.x = newWidth;
        panelTransform.sizeDelta = size;

        UpdateSpacing(newWidth);
    }

    void UpdateSpacing(float width)
    {
        float t = Mathf.InverseLerp(minWidth, maxWidth, width);
        float spacing = Mathf.Lerp(minSpacing, maxSpacing, t);

        foreach (var group in layoutGroups)
        {
            if (group == null) continue;
            group.spacing = spacing;
        }
    }

    public void ResetWidth()
    {
        float defaultWidth = 500f;

        Vector2 size = panelTransform.sizeDelta;
        size.x = defaultWidth;
        panelTransform.sizeDelta = size;

        UpdateSpacing(defaultWidth);
    }
}