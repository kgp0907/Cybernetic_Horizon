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
    private CinemachineFreeLook vcam1;

    [SerializeField]
    private CinemachineVirtualCamera vcam2;

    public bool overworldCamera = true;
    private Animator animator;

    private void Awake()
    {
      //  animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update

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

    private void SwitchState()
    {
        if (overworldCamera)
        {
            animator.Play("FreeLookCamera");
        }
        else
        {
            animator.Play("LockOnCamera");
        }
        overworldCamera = !overworldCamera;
    }

    private void SwitchPriority()
    {
        if (overworldCamera)
        {
            vcam1.Priority = 0;
            vcam2.Priority = 1;
        }
        else
        {
            vcam1.Priority = 1;
            vcam2.Priority = 0;
        }
        overworldCamera = !overworldCamera;
    }

}
