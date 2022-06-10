using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Atk1 : Interface_Base<Player>
{
    protected string slash = "Slash";

    public void OnEnter(Player player)
    {
        player.StartCoroutine(NormalAtk1(player));
    }

    public void OnExit(Player player)
    {

    }

    public void OnFixedUpdate(Player player)
    {
        player.inputmanager.InputMovement();
        player.inputmanager.Rotation();
    }

    public void OnUpdate(Player player)
    {
        player.inputmanager.ComboAtkCheck(Player.playerState.NORMALATK2);
    }

    IEnumerator NormalAtk1(Player player)
    {

        player.animation_id = "NormalAtk1";
        player.m_Animator.SetTrigger(player.animation_id);
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.22f);
        player.AtkColision.SetActive(true);
        GameObject Slash = ObjectPoolingManager.Instance.GetObject(slash, player.EffectSpawnPos[0]);
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.25f);
        player.AtkColision.SetActive(false);
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.5f);
        ObjectPoolingManager.Instance.ReturnObject(slash, Slash);
    }

}
