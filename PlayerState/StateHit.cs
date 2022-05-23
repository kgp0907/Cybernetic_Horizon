using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHit : IState<Player>
{
    public void OnEnter(Player player)
    {
        player.player_Hp.GodMode = true;
        if (player.SmashHit)
        {
            player.animation_id = "Falldown";
            player.PlayerAnimator.SetTrigger("TakeDamage_Knockback");
        }
        else
        {
            player.animation_id = "Hit";
            player.PlayerAnimator.SetTrigger("TakeDamage");
        }
    }

    public void OnExit(Player player)
    {
        player.SmashHit = false;
        player.Hit = false;
        player.player_Hp.GodMode = false;
    }

    public void OnFixedUpdate(Player player)
    {
       
    }

    public void OnUpdate(Player player)
    {
       player.inputmanager.AnimationEndCheck();
    }
}
