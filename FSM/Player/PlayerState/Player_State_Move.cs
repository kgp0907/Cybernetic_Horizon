using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Move : Interface_Base<Player>
{
    public void OnEnter(Player player)
    {

    }

    public void OnExit(Player player)
    {

    }

    public void OnFixedUpdate(Player player)
    {
        player.playerCommand.Move();
        player.playerCommand.Rotation();
    }

    public void OnUpdate(Player player)
    {

    }
}