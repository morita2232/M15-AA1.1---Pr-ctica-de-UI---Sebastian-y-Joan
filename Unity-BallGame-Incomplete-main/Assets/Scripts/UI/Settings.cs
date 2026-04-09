using UnityEngine;

public class Settings : MonoBehaviour
{
    [Header("Panel")]
    public RectTransform panelTransform;

    [Header("Content that should move")]
    public RectTransform contentTransform;

    [Header("Resizer script")]
    public PanelResizer resizer;

    [Header("UI Elements")]
    public GameObject pauseLines;
    public GameObject pauseX;
    public GameObject scaleLines;

    private bool isClicked = false;
    private float truePos = 500f;
    private Vector2 targetPos;

    // ORIGINAL VALUES
    private float originalWidth;
    private Vector2 originalContentPos;

    private void Start()
    {
        targetPos = new Vector2(-truePos, 0);
        panelTransform.anchoredPosition = targetPos;

        // Save original values
        originalWidth = panelTransform.sizeDelta.x;
        originalContentPos = contentTransform.anchoredPosition;
    }

    void Update()
    {
        panelTransform.anchoredPosition =
            Vector2.Lerp(panelTransform.anchoredPosition, targetPos, 10f * Time.unscaledDeltaTime);
    }

    public void ShowPanel()
    {
        isClicked = !isClicked;

        pauseLines.SetActive(!isClicked);
        scaleLines.SetActive(isClicked);
        pauseX.SetActive(isClicked);

        float pos = isClicked ? 0 : -truePos;
        targetPos = new Vector2(pos, 0);

        Time.timeScale = isClicked ? 0f : 1f;

        //  RESET when closing
        if (!isClicked)
        {
            panelTransform.sizeDelta = new Vector2(originalWidth, panelTransform.sizeDelta.y);
            contentTransform.anchoredPosition = originalContentPos;

            // also reset resizer state
            resizer.ResetWidth(originalWidth);
        }
    }




    public void GoSettings()
    {

    }

    public void GoArt()
    {

    }
}