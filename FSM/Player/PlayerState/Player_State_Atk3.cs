using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Atk3 : Interface_Base<Player>
{
    public void OnEnter(Player player)
    {
        player.player_Hp.godMode = true;
        player.StartCoroutine(NormalAtk3Coroutine(player));
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
        player.inputmanager.ComboAtkCheck(Player.playerState.NORMALATK1);
    }

    IEnumerator NormalAtk3Coroutine(Player player)
    {
        player.animation_id = "NormalAtk3";
        player.m_Animator.SetTrigger("NormalAtk3");
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.2f);
        GameObject Slash = ObjectPoolingManager.Instance.GetObject("Slash3", player.EffectSpawnPos[2]);
        player.AtkColision.SetActive(true);
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.25f);
        player.AtkColision.SetActive(false);
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.35f);
        ObjectPoolingManager.Instance.ReturnObject("Slash3", Slash);
        GameObject Smash = ObjectPoolingManager.Instance.GetObject("Smash", player.EffectSpawnPos[3]);
        player.AtkColision.SetActive(true);
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.4f);
        player.AtkColision.SetActive(false);
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.7f);
        ObjectPoolingManager.Instance.ReturnObject("Smash", Smash);
    }
}
