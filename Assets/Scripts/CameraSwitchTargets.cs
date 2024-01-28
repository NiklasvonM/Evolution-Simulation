using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitchTargets : MonoBehaviour
{
    public string targetTag = "PossibleCameraTarget";
    public bool automaticallSwitchBack = false;
    public CinemachineFreeLook freeLookCamera;

    private Transform currentFollowTarget;
    private Transform currentLookAtTarget;

    private void Start()
    {
        if (freeLookCamera == null)
        {
            freeLookCamera = GetComponent<CinemachineFreeLook>();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag(targetTag))
                {
                    // Switch the camera targets
                    currentFollowTarget = freeLookCamera.Follow;
                    currentLookAtTarget = freeLookCamera.LookAt;

                    freeLookCamera.Follow = hit.collider.transform;
                    freeLookCamera.LookAt = hit.collider.transform;

                    // Restore the previous targets after a delay (optional)
                    if (automaticallSwitchBack)
                        StartCoroutine(RestoreTargetsAfterDelay(2.0f));
                }
            }
        }
    }

    private IEnumerator RestoreTargetsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        freeLookCamera.Follow = currentFollowTarget;
        freeLookCamera.LookAt = currentLookAtTarget;
    }
}
