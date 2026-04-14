using UnityEngine;

public class UISafeAreaController : MonoBehaviour
{
    public RectTransform panel;

    [Range(0f, 1f)]
    public float horizontalPadding = 0f;

    [Range(0f, 1f)]
    public float verticalPadding = 0f;
    private void Start()
    {
        Apply();
    }
    public void Apply()
    {
        Rect safe = Screen.safeArea;

        float padX = horizontalPadding * safe.width;
        float padY = verticalPadding * safe.height;

        Vector2 min = safe.position;
        Vector2 max = safe.position + safe.size;

        min.x += padX;
        max.x -= padX;

        min.y += padY;
        max.y -= padY;

        // Convertir a anchors (0 - 1)
        min.x /= Screen.width;
        min.y /= Screen.height;
        max.x /= Screen.width;
        max.y /= Screen.height;

        panel.anchorMin = min;
        panel.anchorMax = max;

        panel.offsetMin = Vector2.zero;
        panel.offsetMax = Vector2.zero;
    }
}
