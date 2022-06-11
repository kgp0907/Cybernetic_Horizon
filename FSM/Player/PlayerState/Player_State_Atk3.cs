using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Atk3 : Interface_Base<Player>
{
    private GameObject slashEffect3_Obj;
    private GameObject slashEffect4_Obj;
    private string slashEffect3 = "slashEffect3";
    private string slashEffect4 = "slashEffect4";

    public void OnEnter(Player player)
    {
        player.player_Hp.godMode = true;
        player.isAttacking = true;
        player.StartCoroutine(NormalAtk3Coroutine(player));
    }

    public void OnExit(Player player)
    {
        player.isAttacking = false;
        player.player_Hp.godMode = false;
        player.atkIndex = 1;
    }

    public void OnFixedUpdate(Player player)
    {

    }

    public void OnUpdate(Player player)
    {
        player.playerCommand.AnimationEndCheck();
    }

    IEnumerator NormalAtk3Coroutine(Player player)
    {
        player.animation_id = "NormalAtk3";
        player.atkIndex = 3;

        player.m_Animator.SetTrigger(player.animation_id);
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.2f);
        slashEffect3_Obj = ObjectPoolingManager.Instance.GetObject(slashEffect3, player.EffectSpawnPos[2]);
        player.AtkColision.SetActive(true);

        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.25f);
        player.AtkColision.SetActive(false);

        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.35f);
        ObjectPoolingManager.Instance.ReturnObject(slashEffect3, slashEffect3_Obj);
        slashEffect4_Obj = ObjectPoolingManager.Instance.GetObject(slashEffect4, player.EffectSpawnPos[3]);
        player.AtkColision.SetActive(true);

        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.4f);
        player.AtkColision.SetActive(false);

        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.7f);
        ObjectPoolingManager.Instance.ReturnObject(slashEffect4, slashEffect4_Obj);
    }
}
