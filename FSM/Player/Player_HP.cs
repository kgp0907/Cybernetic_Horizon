using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIHealthAlchemy;
public class Player_HP : MonoBehaviour
{
    private Player player;
    public float playerHP;
    public bool godMode = false;

    public ElemsHealthBar hpBar;
    public ElemsHealthBar mpBar;
    private string healEffect = "HealEffect";
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        hpBar.Value = playerHP * 0.01f;
    }

    public void PlayerTakeDamage(float damage)
    {
        if (godMode)
            return;

        hpBar.Value -= damage * 0.01f;

        if (hpBar.Value <= 0)
        {
            player.ChangeState(Player.playerState.DEAD);
        }
        else
        {
            player.ChangeState(Player.playerState.HIT);
        }
    }

    public void HealingHP()
    {
        GameObject HealEffect = ObjectPoolingManager.Instance.GetObject(healEffect, player.EffectSpawnPos[4]);
        hpBar.Value = playerHP * 0.01f;
        StartCoroutine(ReturnEffect(HealEffect));
    }

    IEnumerator ReturnEffect(GameObject HealEffect)
    {
        yield return StaticCoroutine.Wait(2f);
        ObjectPoolingManager.Instance.ReturnObject(healEffect, HealEffect);
    }
}
