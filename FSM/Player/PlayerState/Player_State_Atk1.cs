using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Atk1 : Interface_Base<Player>
{
    private string normalAtk1 = "NormalAtk1";
    private GameObject slashEffect_Obj;
    private GameObject slashEffect_Obj2;
    private string slashEffect = "slashEffect";

    public void OnEnter(Player player)
    {
        player.StartCoroutine(NormalAtk1(player));
    }

    public void OnExit(Player player)
    {
        player.AtkColision.SetActive(false);
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

    IEnumerator NormalAtk1(Player player)
    {
        player.animation_id = "NormalAtk1";
        player.atkIndex = 1;
        player.isAttacking = true;
        player.m_Animator.SetTrigger(player.animation_id);

        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.24f);
        player.AtkColision.SetActive(true);
        slashEffect_Obj = ObjectPoolingManager.Instance.GetObject(slashEffect, player.EffectSpawnPos[0]);

        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.3f);
        player.AtkColision.SetActive(false);

        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.5f);
        ObjectPoolingManager.Instance.ReturnObject(slashEffect, slashEffect_Obj);
        player.isAttacking = false;
    }

}
