using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Dodge : Interface_Base<Player>
{
    public void OnEnter(Player player)
    {
        player.player_Hp.godMode = true;
        player.animation_id = "Dumb";
        player.m_Animator.SetTrigger("Dumb");
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
