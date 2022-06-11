using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_ChargeAtk : Interface_Base<Player>
{
    private GameObject chargeEffect_Obj;
    private string chargeEffect = "chargeEffect";

    public void OnEnter(Player player)
    {
        player.playerDamage *= 2;
        player.player_Hp.godMode = true;
        player.StartCoroutine(ChargeAtk(player));
    }

    public void OnExit(Player player)
    {
        player.playerDamage *= 0.5f;
        player.player_Hp.godMode = false;
    }

    public void OnFixedUpdate(Player player)
    {

    }

    public void OnUpdate(Player player)
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            player.ChangeState(Player.playerState.DODGE);
        }
    }

    IEnumerator ChargeAtk(Player player)
    {
        player.animation_id = "ChargeAtk";
        player.m_Animator.SetTrigger(player.animation_id);
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.3f);
        player.AtkColision.SetActive(true);
        chargeEffect_Obj = ObjectPoolingManager.Instance.GetObject(chargeEffect, player.EffectSpawnPos[2]);

        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.31f);
        player.AtkColision.SetActive(false);
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.33f);
        player.AtkColision.SetActive(true);
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.35f);
        player.AtkColision.SetActive(false);
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.37f);
        player.AtkColision.SetActive(true);
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.39f);
        player.AtkColision.SetActive(false);
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.41f);
        player.AtkColision.SetActive(true);
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.43f);
        player.AtkColision.SetActive(false);

        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.9f);
        ObjectPoolingManager.Instance.ReturnObject(chargeEffect, chargeEffect_Obj);
        player.ChangeState(Player.playerState.MOVE);
    }
}
