using UnityEngine;

public class UISafeAreaController : MonoBehaviour
{
    public RectTransform panel;

    [Range(0f, 1f)]
    public float horizontalPadding = 0f;

    [Range(0f, 1f)]
    public float verticalPadding = 0f;

    public void Apply()
    {
        float padX = horizontalPadding * Screen.width;
        float padY = verticalPadding * Screen.height;

        Rect safe = Screen.safeArea;

        Vector2 min = safe.position;
        Vector2 max = safe.position + safe.size;

        min.x += padX;
        max.x -= padX;

        min.y += padY;
        max.y -= padY;
    }
}
