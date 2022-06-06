using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    [SerializeField]
    private InputAction action;

    [SerializeField]
    private CinemachineFreeLook FreeLookCamera;

    [SerializeField]
    private CinemachineVirtualCamera LockOnCamera;

    public bool isLockOnCamera = true;

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }
    void Start()
    {
        action.performed += _ => SwitchPriority();   
    }

    // ī�޶��� �켱���� �����ϴ� �޼���
    private void SwitchPriority()
    {
        if (isLockOnCamera)
        {
            FreeLookCamera.Priority = 1;
            LockOnCamera.Priority = 0;
        }
        else
        {
            FreeLookCamera.Priority = 0;
            LockOnCamera.Priority = 1;
        }
        isLockOnCamera = !isLockOnCamera;
    }
}


