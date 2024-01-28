using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RightMouseCameraControl : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float sensitivity = 2.0f;

    private bool isRightMouseButtonDown = false;

    private void Start()
    {
        if (freeLookCamera == null)
        {
            freeLookCamera = GetComponent<CinemachineFreeLook>();
        }
    }

    private void Update()
    {
        // Check for right mouse button input
        if (Input.GetMouseButtonDown(1))
        {
            isRightMouseButtonDown = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isRightMouseButtonDown = false;
        }

        freeLookCamera.enabled = isRightMouseButtonDown;
    }
}
