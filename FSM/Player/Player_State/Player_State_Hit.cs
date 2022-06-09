using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Hit : Interface_Base<Player>
{
    public void OnEnter(Player player)
    {
        player.player_Hp.godMode = true;
        player.animation_id = "Falldown";
        player.m_Animator.SetTrigger("TakeDamage_Knockback");

    }

    public void OnExit(Player player)
    {
        player.isSmashHit = false;
        player.isHit = false;
        player.player_Hp.godMode = false;
    }

    public void OnFixedUpdate(Player player)
    {

    }

    public void OnUpdate(Player player)
    {
        player.inputmanager.AnimationEndCheck();
    }
}
