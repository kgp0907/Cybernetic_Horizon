using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Dead : Interface_Base<Player>
{
    public void OnEnter(Player player)
    {
        player.m_Animator.SetTrigger("Dead");
        player.GetComponent<CharacterController>().enabled = false;
    }

    public void OnExit(Player player)
    {

    }

    public void OnFixedUpdate(Player player)
    {

    }

    public void OnUpdate(Player player)
    {

    }
}