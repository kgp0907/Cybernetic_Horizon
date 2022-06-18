using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Hit : Interface_Base<Player>
{
    public void OnEnter(Player player)
    {
        player.player_Stats.godMode = true;
        player.animation_id = "Falldown";
        player.m_Animator.SetTrigger("TakeDamage_Knockback");
    }

    public void OnExit(Player player)
    {
        player.isSmashHit = false;
        player.isHit = false;
        player.player_Stats.godMode = false;
    }

    public void OnFixedUpdate(Player player)
    {

    }

    public void OnUpdate(Player player)
    {
        player.playerCommand.AnimationEndCheck();
        player.player_Stats.HealingStamina();
    }
}