using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Move : Base_Interface<Player>
{
    public void OnEnter(Player player)
    {
    
    }

    public void OnExit(Player player)
    {
       
    }

    public void OnFixedUpdate(Player player)
    {
        player.inputmanager.InputMovement();
        player.playerController.Move(player.inputmanager.velocity * Time.deltaTime);
        player.inputmanager.Rotation();
    }

    public void OnUpdate(Player player)
    {

        if (Input.GetMouseButtonDown(0))
        {
            player.ChangeState(Player.playerState.NORMALATK1);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            player.ChangeState(Player.playerState.DODGE);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            player.ChangeState(Player.playerState.CHARGEATK);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (player.useInventory)
            {
                player.OnApplicationFocus(player.useInventory);
                player.useInventory = false;
                player.inventoryUI.SetActive(false);
            }
           
            else
            {
                player.OnApplicationFocus(player.useInventory);
                player.useInventory = true;                  
                player.inventoryUI.SetActive(true);
            }
               
        }
    }

}

