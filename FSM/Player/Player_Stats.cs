using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIHealthAlchemy;
public class Player_Stats : MonoBehaviour
{
    private Player player;
    public float playerHP;
    public float playerStamina;
    public float playerDamage;
   
    public float staminaRegen=1f;

    private float maxStamina=1f;
    private float staminaRegenValue;
    public bool godMode = false;

    public ElemsHealthBar hpBar;
    public ElemsHealthBar staminaBar;
    private string healEffect = "HealEffect";
    private void Awake()
    {
        player = FindObjectOfType<Player>();

        hpBar.Value = playerHP * 0.01f;
        staminaBar.Value = playerStamina * 0.01f;
        staminaRegenValue = staminaRegen * 0.01f;
    }

    public void PlayerTakeDamage(float damage)
    {
        if (godMode)
            return;

        hpBar.Value -= damage *0.01f;
        CinemachineImpulse.Instance.CameraShake(5f);
        if (hpBar.Value <= 0)
        {
            player.ChangeState(Player.playerState.DEAD);
        }
        else
        {
            player.ChangeState(Player.playerState.HIT);
        }
    }

    public void UseStamina(float stamina)
    {
        staminaBar.Value -= stamina * 0.01f;
    }

    public void HealingHP()
    {
        GameObject HealEffect= ObjectPoolingManager.Instance.GetObject(healEffect, player.EffectSpawnPos[4]);
        hpBar.Value = playerHP*0.01f;
        StartCoroutine(ReturnEffect(HealEffect));
    }

    public void HealingStamina()
    {
        if (maxStamina > staminaBar.Value)
        {
            staminaBar.Value += staminaRegenValue*Time.deltaTime;

            if (maxStamina <= staminaBar.Value)
                staminaBar.Value = maxStamina;
        }      
    }

    IEnumerator ReturnEffect(GameObject HealEffect) 
    {
        yield return StaticCoroutine.Wait(2f);
        ObjectPoolingManager.Instance.ReturnObject(healEffect, HealEffect);
    }
}
