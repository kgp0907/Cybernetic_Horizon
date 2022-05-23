using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIHealthAlchemy;
public class Player_HP : MonoBehaviour
{
    private Player player;
    public Animator playani;
    public float PlayerHp;
    public bool GodMode = false;

    public ElemsHealthBar HpBar;
    public ElemsHealthBar MpBar;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        HpBar.Value = PlayerHp / 100;
    }

    public void PlayerTakeDamage(float damage)
    {
        if (GodMode)
            return;
        HpBar.Value -= damage / PlayerHp;

        if (HpBar.Value <= 0)
        {
            player.ChangeState(Player.eState.DEAD);
        }
        else
        {
            player.ChangeState(Player.eState.HIT);
        }

    }

    public void PlayerHeal()
    {
        GameObject HealEffect= ObjectPoolingManager.Instance.GetObject("HealEffect", player.EffectSpawnPos[4]);
        HpBar.Value = PlayerHp / 100;
        StartCoroutine(ReturnEffect(HealEffect));
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("EnemyAtk_Smash") && !GodMode)
        {
            player.SmashHit = true;
        }

        else if (other.gameObject.CompareTag("EnemyAtk") && !GodMode)
        {
            player.Hit = true;
        }

    }

    IEnumerator ReturnEffect(GameObject HealEffect) 
    {
        yield return new WaitForSeconds(2f);
        ObjectPoolingManager.Instance.ReturnObject("HealEffect", HealEffect);
    }
}
