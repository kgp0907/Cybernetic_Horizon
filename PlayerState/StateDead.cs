using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDead : IState<Player>
{
    public void OnEnter(Player player)
    {
        //player.p_takedamage.CopyTransformRagdoll(player.Character.transform, player.RagDoll.transform);
       
        player.Character.SetActive(false);
     
        player.PlayerAnimator.SetTrigger("Dead");
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
