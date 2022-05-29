using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Dodge : Base_Interface<Player>
{
    public void OnEnter(Player player)
    {
        player.player_Hp.godMode = true;
        if (player.isLockOn)
        {
            player.animation_id = "Slide";
            player.playerAnimator.SetTrigger("Slide");
        }

        else
        {
            player.animation_id = "Dumb";
            player.playerAnimator.SetTrigger("Dumb");
        }         
    }

    public void OnExit(Player player)
    {
        player.player_Hp.godMode = false;
    }

    public void OnFixedUpdate(Player player)
    {
        player.inputmanager.InputMovement();
        player.inputmanager.Rotation();
    }

    public void OnUpdate(Player player)
    {
       player.inputmanager.AnimationEndCheck();    
    }
    
    
}
