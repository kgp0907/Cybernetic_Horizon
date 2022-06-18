using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Move : Interface_Base<Player>
{
    public void OnEnter(Player player)
    {
        player.isMove = true;
    }

    public void OnExit(Player player)
    {
        player.isMove = false;
    }

    public void OnFixedUpdate(Player player)
    {
        player.playerCommand.Move();
        player.playerCommand.Rotation();
    }

    public void OnUpdate(Player player)
    {
        player.player_Stats.HealingStamina();
    }
}