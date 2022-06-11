using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerCommand : MonoBehaviour
{
    //인풋 필드
    private Player player;
    [SerializeField]
    private PlayerActionAsset playerActionAsset;
    public Transform cameraTransform;

    //이동 필드
    private Vector3 moveDirection;
    public Vector3 velocity;
    float maximumSpeed = 25;
    public float rotationSpeed=1000;
   
    //캐싱 필드
    float horizontalInput; 
    float verticalInput;
    string horizontal = "Horizontal";
    string vertical = "Vertical";

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        playerActionAsset = new PlayerActionAsset();   
    }

    private void OnEnable()
    {
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
        player.transform.position += velocity * Time.deltaTime;
    }

    public void DoAttack(InputAction.CallbackContext obj)
    {
        if (player.AnimationName && player.AnimationProgress >= 0.6f)
        {
            {
                if(player.atkIndex==1)
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

    private void DoChargeAtk(InputAction.CallbackContext obj)
    {
        player.ChangeState(Player.playerState.CHARGEATK);
    }

    public void DoDodge(InputAction.CallbackContext obj)
    {
        if(player.player_Hp.godMode==false)
        player.ChangeState(Player.playerState.DODGE);
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
