using UnityEngine;

public class Settings : MonoBehaviour
{
    public RectTransform panelTransform;
    public GameObject pauseLines;
    public GameObject pauseX;

    private bool isClicked = false;
    private float truePos = 250f;
    private Vector2 targetPos;

    private void Start()
    {
        targetPos = new Vector2(-truePos, 0);

        panelTransform.anchoredPosition = targetPos;
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
        pauseX.SetActive(isClicked);
        

        float pos = isClicked ? truePos : -truePos;
        targetPos = new Vector2(pos, 0);

        Time.timeScale = isClicked ? 0f : 1f;
    }
}
