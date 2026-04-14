using UnityEngine;

public class CameraChanger : MonoBehaviour
{

    public float targetNum;
    public CameraController camController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        targetNum = 0f;

    }

    // Update is called once per frame
    void Update()
    {

        switch (targetNum) {

            case 0:
                camController.SwitchTargetBall();
                break;

            case 1:
                camController.SwitchTargetTarget();
                break;

            case 2:
                camController.SwitchTargetMiddlepoint();
                break;               
        
        }

    }


    public void NumChange()
    {

        targetNum += 1;

        if (targetNum > 2)
        {
            targetNum = 0;
        }

    }


}
