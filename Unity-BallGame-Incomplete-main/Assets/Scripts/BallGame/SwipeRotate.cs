using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeRotate : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 0.2f;
    public float smoothSpeed = 10f;

    private float desiredRotation = 0f;
    private float lastX;
    private bool isDragging = false;

    void Update()
    {
        Debug.Log("EXISTO");
        if (IsPointerOverUI())
            return;

        HandleInput();

        
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(0, desiredRotation, 0),
            Time.deltaTime * smoothSpeed
        );
    }

    void HandleInput()
    {
        
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isDragging = true;
                lastX = touch.position.x;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                float deltaX = touch.position.x - lastX;
                lastX = touch.position.x;

                Rotate(deltaX);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }

            return; 
        }

        
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            float deltaX = Input.mousePosition.x - lastX;
            lastX = Input.mousePosition.x;

            Rotate(deltaX);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    public void Rotate(float value)
    {
        desiredRotation += value * rotationSpeed;
    }

    
    bool IsPointerOverUI()
    {
        // Mouse
        if (EventSystem.current.IsPointerOverGameObject())
            return true;

        // Touch
        if (Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return true;
        }

        return false;
    }
}