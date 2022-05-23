using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDodge : IState<Player>
{
    public void OnEnter(Player player)
    {
        player.player_Hp.GodMode = true;
        if (player.isLockOn)
        {
            player.animation_id = "Slide";
            player.PlayerAnimator.SetTrigger("Slide");
        }

        else
        {
            player.animation_id = "Dumb";
            player.PlayerAnimator.SetTrigger("Dumb");
        }         
    }

    public void OnExit(Player player)
    {
        player.player_Hp.GodMode = false;
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
