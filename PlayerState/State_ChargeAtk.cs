using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_ChargeAtk : IState<Player>
{
    public void OnEnter(Player player)
    {
        player.playerDamage *= 2;
        player.player_Hp.GodMode = true;
        player.StartCoroutine(NormalAtk1Coroutine(player));
    }

    public void OnExit(Player player)
    {
        player.playerDamage /= 2;
        player.player_Hp.GodMode = false;
    }

    public void OnFixedUpdate(Player player)
    {
        player.inputmanager.InputMovement();
        player.inputmanager.Rotation();
    }

    public void OnUpdate(Player player)
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            player.ChangeState(Player.eState.DODGE);
        }
    }

    IEnumerator NormalAtk1Coroutine(Player player)
    {
        player.animation_id = "ChargeAtk";
        player.PlayerAnimator.SetTrigger("ChargeAtk");
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.3f);
        player.AtkColision.SetActive(true);
        GameObject Slash2 = ObjectPoolingManager.Instance.GetObject("Slash2", player.EffectSpawnPos[2]);
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.31f);
        player.AtkColision.SetActive(false);
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.33f);
        player.AtkColision.SetActive(true);
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.35f);
        player.AtkColision.SetActive(false);
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.37f);
        player.AtkColision.SetActive(true);
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.39f);
        player.AtkColision.SetActive(false);
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.41f);
        player.AtkColision.SetActive(true);
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.43f);
        player.AtkColision.SetActive(false);
        yield return new WaitUntil(() => player.AnimationName && player.AnimationProgress >= 0.9f);
        ObjectPoolingManager.Instance.ReturnObject("Slash2", Slash2);
        player.ChangeState(Player.eState.MOVE);
    }
}
