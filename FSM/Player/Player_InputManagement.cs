using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player_InputManagement : MonoBehaviour
{
    public float rotationSpeed;
    public Transform cameraTransform;
    private Vector3 moveDirection;
    public Vector3 velocity; 
    private float maximumSpeed = 16f;
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    public void InputMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(moveDirection.magnitude);

        player.playerAnimator.SetFloat("horizontal", horizontalInput, 0.1f, Time.deltaTime);
        player.playerAnimator.SetFloat("vertical", verticalInput, 0.1f, Time.deltaTime);

        float speed = inputMagnitude * maximumSpeed;

        moveDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;
        moveDirection.Normalize();

        velocity = moveDirection * speed;
    }
    public void Rotation()
    {
        if (moveDirection !=  Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void ComboAtkCheck(Player.playerState state)
    {
        if (player.AnimationName && player.AnimationProgress >= 0.6f && Input.GetMouseButtonDown(0))
        {
            player.ChangeState(state);
        }
        if (player.AnimationName && player.AnimationProgress >= 0.9f)
        {
            player.ChangeState(Player.playerState.MOVE);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            player.ChangeState(Player.playerState.DODGE);
        }
    }
    public void AnimationEndCheck()
    {
        if (player.AnimationName && player.AnimationProgress >= 0.9f)
        {
            player.ChangeState(Player.playerState.MOVE);
        }
    }
}
