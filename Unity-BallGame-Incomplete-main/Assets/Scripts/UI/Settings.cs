using UnityEngine;

public class Settings : MonoBehaviour
{
    public RectTransform panelTransform;
    public PanelResizer resizer;

    public GameObject pauseLines;
    public GameObject pauseX;
    public GameObject scaleLines;

    private bool isOpen = false;

    private float closedX = -1440f;
    private float openX = -950f;

    void Start()
    {
        panelTransform.anchoredPosition = new Vector2(closedX, 0);

        // lock width
        panelTransform.sizeDelta = new Vector2(500f, panelTransform.sizeDelta.y);
    }

    void Update()
    {
        float targetX = isOpen ? openX : closedX;

        Vector2 pos = panelTransform.anchoredPosition;

        pos.x = Mathf.Lerp(pos.x, targetX, 10f * Time.unscaledDeltaTime);

        panelTransform.anchoredPosition = pos;
    }

    public void TogglePanel()
    {
        isOpen = !isOpen;

        pauseLines.SetActive(!isOpen);
        scaleLines.SetActive(isOpen);
        pauseX.SetActive(isOpen);

        Time.timeScale = isOpen ? 0f : 1f;

        if (!isOpen)
        {
            resizer.ResetWidth();
        }
    }
}