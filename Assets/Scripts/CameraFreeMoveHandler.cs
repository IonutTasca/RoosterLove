using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class CameraFreeMoveHandler : MonoBehaviour
{
    [SerializeField] private float touchSensitivity_x = 10f;
    [SerializeField] private float touchSensitivity_y = 10f;

    [SerializeField] private List<RectTransform> ignoreRectsArea;


    void Start()
    {
        CinemachineCore.GetInputAxis = HandleAxisInputDelegate;
    }


    float HandleAxisInputDelegate(string axisName)
    {
        if (Input.touchCount == 0)
            return 0f;

        foreach(RectTransform rectTransform in ignoreRectsArea)
            if (!RectTransformUtility.RectangleContainsScreenPoint(rectTransform, (Input.GetTouch(0).position)))
                return 0f;

        int cameraTouchId = 0;
        foreach(RectTransform rectTransform in ignoreRectsArea)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (!RectTransformUtility.RectangleContainsScreenPoint(rectTransform, (Input.GetTouch(0).position)))
                    continue;
                cameraTouchId = i;
            }
        }
        

        switch (axisName)
        {
            case "Mouse X":

                if (Input.touchCount > 0)
                {
                    return Input.touches[cameraTouchId].deltaPosition.x / touchSensitivity_x;
                }
                else
                {
                    return Input.GetAxis(axisName);
                }

            case "Mouse Y":
                if (Input.touchCount > 0)
                {
                    return Input.touches[cameraTouchId].deltaPosition.y / touchSensitivity_y;
                }
                else
                {
                    return Input.GetAxis(axisName);
                }

            default:
                Debug.LogError("Input <" + axisName + "> not recognyzed.", this);
                break;
        }

        return 0f;
    }

}
