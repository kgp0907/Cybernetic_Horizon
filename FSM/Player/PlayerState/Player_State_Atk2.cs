using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Atk2 : Interface_Base<Player>
{
    private GameObject slashEffect_Obj;
    private string slashEffect = "slashEffect";

    public void OnEnter(Player player)
    {
        player.StartCoroutine(NormalAtk2Coroutine(player));
    }

    public void OnExit(Player player)
    {
        player.isAttacking = false;
        player.atkIndex = 1;
    }

    public void OnFixedUpdate(Player player)
    {
        player.playerCommand.Move();
        player.playerCommand.Rotation();
    }

    public void OnUpdate(Player player)
    {
        player.playerCommand.AnimationEndCheck();
    }

    IEnumerator NormalAtk2Coroutine(Player player)
    {
        player.animation_id = "NormalAtk2";
        player.atkIndex = 2;
        player.m_Animator.SetTrigger(player.animation_id);
        player.isAttacking = true;
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.16f);
        slashEffect_Obj = ObjectPoolingManager.Instance.GetObject(slashEffect, player.EffectSpawnPos[1]);
        player.AtkColision.SetActive(true);

        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.23f);
        player.AtkColision.SetActive(false);
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.5f);
        player.isAttacking = false;
        ObjectPoolingManager.Instance.ReturnObject(slashEffect, slashEffect_Obj);


    }
}
