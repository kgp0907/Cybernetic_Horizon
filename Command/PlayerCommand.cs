using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerCommand : MonoBehaviour
{
    //인풋 필드
    [SerializeField]
    private CinemachineFreeLook FreeLookCamera;

    [SerializeField]
    private CinemachineVirtualCamera LockOnCamera;

    private Player player;
    private Player_Stats player_HP;
    [SerializeField]
    private PlayerActionAsset playerActionAsset;
    public Transform cameraTransform;

    //이동 필드
    private Vector3 moveDirection;
    private Vector3 velocity;
    public float dashStaminaCost = 1f;
    public float atkStaminaCost = 20f;
    public float dodgeStaminaCost = 20f;
    public float maximumSpeed = 25;
    public float rotationSpeed = 1000;

    public bool isLockOnCamera = true;
    //캐싱 필드
    float horizontalInput;
    float verticalInput;
    string horizontal = "Horizontal";
    string vertical = "Vertical";

    private void Awake()
    {
        player_HP = FindObjectOfType<Player_Stats>();
        player = FindObjectOfType<Player>();
        playerActionAsset = new PlayerActionAsset();
    }

    private void OnEnable()
    {
        playerActionAsset.Player.LockTarget.started += SwitchPriority;
        playerActionAsset.Player.Attack.started += DoAttack;
        playerActionAsset.Player.ChargeAttack.started += DoChargeAtk;
        playerActionAsset.Player.Dodge.started += DoDodge;
        playerActionAsset.Player.Inventory.started += UseInventory;
        playerActionAsset.Player.Enable();
    }

    public void Move()
    {
        horizontalInput = Input.GetAxis(horizontal);
        verticalInput = Input.GetAxis(vertical);

        moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(moveDirection.magnitude);

        player.m_Animator.SetFloat(horizontal, horizontalInput, 0.1f, Time.deltaTime);
        player.m_Animator.SetFloat(vertical, verticalInput, 0.1f, Time.deltaTime);

        float speed = inputMagnitude * maximumSpeed;

        moveDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;
        moveDirection.Normalize();

        velocity = moveDirection * speed;
        if (player.isMove)
        {
            player.transform.position += velocity * Time.deltaTime;
        }
    }

    public void DoAttack(InputAction.CallbackContext obj)
    {
        if (atkStaminaCost * 0.01f < player_HP.staminaBar.Value && player.isAttacking == false)
        {
            player_HP.UseStamina(atkStaminaCost);
            if (player.AnimationName && player.AnimationProgress >= 0.6f)
            {
                {
                    if (player.atkIndex == 1)
                        player.ChangeState(Player.playerState.NORMALATK2);
                    else if (player.atkIndex == 2)
                        player.ChangeState(Player.playerState.NORMALATK3);
                    else if (player.atkIndex == 3)
                        player.ChangeState(Player.playerState.NORMALATK1);
                }
            }
            else
            {
                player.ChangeState(Player.playerState.NORMALATK1);
            }
        }
    }

    private void DoChargeAtk(InputAction.CallbackContext obj)
    {
        if (player.isAttacking == false)
            player.ChangeState(Player.playerState.CHARGEATK);
    }

    public void DoDodge(InputAction.CallbackContext obj)
    {
        if (player.player_Stats.godMode == false && dodgeStaminaCost * 0.01f < player_HP.staminaBar.Value)
        {
            player.ChangeState(Player.playerState.DODGE);
            player_HP.UseStamina(dodgeStaminaCost);
        }

    }

    private void SwitchPriority(InputAction.CallbackContext obj)
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

    public void UseInventory(InputAction.CallbackContext obj)
    {
        if (player.useInventory == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            player.OnApplicationFocus(player.useInventory);
            player.useInventory = false;
            player.inventoryUI.SetActive(false);

        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            player.OnApplicationFocus(player.useInventory);
            player.useInventory = true;
            player.inventoryUI.SetActive(true);
        }
    }

    public void Rotation()
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void AnimationEndCheck()
    {
        if (player.AnimationName && player.AnimationProgress >= 0.9f)
        {
            player.ChangeState(Player.playerState.MOVE);
        }
    }

    private void OnDisable()
    {
        playerActionAsset.Player.Disable();
    }
}
