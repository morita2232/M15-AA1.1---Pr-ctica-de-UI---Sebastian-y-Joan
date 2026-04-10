using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class SettingValueController : MonoBehaviour
{
    [Header("Value Settings")]
    public float value = 50f;
    public float minValue = 0f;
    public float maxValue = 100f;
    public float step = 5f;
    private bool isUpdating = false;

    [Header("UI")]
    public TMP_InputField inputField;
    public Slider slider;
    public TextMeshProUGUI valueText;

    [Header("Events")]
    public UnityEvent<float> onValueChanged;

    [Header("Safe Area")]
    public UISafeAreaController safeArea;

    private void Start()
    {
        if (slider != null)
        {
            slider.minValue = minValue;
            slider.maxValue = maxValue;
        }

        UpdateUI();
        Apply();
    }

    // Botón +
    public void Increase()
    {
        value += step;
        ClampValue();
        UpdateUI();
    }

    // Botón -
    public void Decrease()
    {
        value -= step;
        ClampValue();
        UpdateUI();
    }

    // Cuando escribes en el input
    public void OnInputChanged(string input)
    {
        if (isUpdating) return; // Evitar loop infinito
        if (float.TryParse(input, out float newValue))
        {
            value = newValue;
            ClampValue();
            UpdateAll();
        }
    }

    // Cuando mueves slider
    public void OnSliderChanged(float newValue)
    {
        if(isUpdating) return; // Evitar loop infinito
        value = newValue;
        ClampValue();
        UpdateAll();
    }

    void ClampValue()
    {
        value = Mathf.Clamp(value, minValue, maxValue);
    }


    // Ajustar padding vertical del safe area
    public void SetHorizontal(float v)
    {
        safeArea.horizontalPadding = v;
        safeArea.Apply();
    }
    // Ajustar padding horizontal del safe area
    public void SetVertical(float v)
    {
        safeArea.verticalPadding = v;
        safeArea.Apply();
    }



    void UpdateAll()
    {
        UpdateUI();
        Apply();
        onValueChanged?.Invoke(value);
    }
    void UpdateUI()
    {
        isUpdating = true;

        if (inputField != null)
            inputField.text = value.ToString("0.00"); // Numero de decimales

        if (valueText != null)
            valueText.text = value.ToString("0.00");

        if (slider != null)
            slider.value = value;

        isUpdating = false;
    }
    void Apply()
    {
        onValueChanged.Invoke(value);
    }
}
