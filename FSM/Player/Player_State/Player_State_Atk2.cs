using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Atk2 : Interface_Base<Player>
{
    public void OnEnter(Player player)
    {
        player.StartCoroutine(NormalAtk2Coroutine(player));
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
        player.inputmanager.ComboAtkCheck(Player.playerState.NORMALATK3);
    }

    IEnumerator NormalAtk2Coroutine(Player player)
    {
        player.animation_id = "NormalAtk2";
        player.m_Animator.SetTrigger("NormalAtk2");
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.3f);
        GameObject Slash = ObjectPoolingManager.Instance.GetObject("Slash", player.EffectSpawnPos[1]);
        player.AtkColision.SetActive(true);
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.33f);
        player.AtkColision.SetActive(false);
        yield return StaticCoroutine.WaitUntil(player.animation_id, player.m_Animator, 0.7f);
        ObjectPoolingManager.Instance.ReturnObject("Slash", Slash);
    }
}
