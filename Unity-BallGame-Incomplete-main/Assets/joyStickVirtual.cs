using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;

public class joyStickVirtual : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{

    public RectTransform joyStickParent;
    public RectTransform joystick;
    public RectTransform joystickGhost;

    public float maxRadius;

    public Vector2 input;

    public bool reposition;

    public BallController ballController;

    InputSystem inputSystem;

    void Awake()
    {
        inputSystem = new InputSystem();
    }


    void OnEnable()
    {
        inputSystem.Ball.Enable();
    }

    void OnDisable()
    {
        inputSystem.Ball.Disable();
    }

    void Update()
    {
        Vector2 dirWASD = inputSystem.Ball.Movement.ReadValue<Vector2>();
        ballController.Move(dirWASD);

        if (inputSystem.Ball.Jump.WasPressedThisFrame())
        {
            ballController.Jump();
        }

    }

    public void OnBeginDrag(PointerEventData data)
    {

        if (reposition)
        {
            joyStickParent.position = data.position;
        }

        joystick.position = data.position;
        joystickGhost.position = data.position;

    }

   public void OnDrag(PointerEventData data)
    {
        

        joystick.position = data.position;
        joystickGhost.position = data.position;

        Vector3 dir = joystick.position - joyStickParent.position;

        float distance = dir.magnitude;

        Debug.Log(distance);
        
        if(distance > maxRadius )
        {
            Debug.Log("Te has pasado");

            dir.Normalize();

            dir *= maxRadius;

            joystick.localPosition = dir;

        }

        dir /= maxRadius;

        input = dir;
        
        ballController.Move(input);

    }

    public void OnEndDrag(PointerEventData data)
    {
        
        joystick.localPosition = Vector3.zero;
        joystickGhost.localPosition = Vector3.zero;

        input = Vector2.zero;
    }


}
