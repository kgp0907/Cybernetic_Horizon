using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy_HP : MonoBehaviour
{
    Robot_Base robotp1;
    public float hp_current = 200;
    public int hp;
    public float EffectReturnTime = 2f;
    public bool dead;
    public Image healthBarFilled;
    public GameObject healthBarBackground;

    private void Start()
    {
        hp_current = hp;
        healthBarFilled.fillAmount = 1f;
        robotp1 = GetComponent<Robot_Base>();
    }

    public void TakeDamage(float damage)
    {
        hp_current -= damage;

        if (hp_current <= 0 && !dead)
        {
            robotp1 = Robot_Base.FindObjectOfType<Robot_Base>();
            dead = true;
            robotp1.StopAllCoroutines();
            robotp1.ChangeState(Robot_Base.Robot_State.DEAD);
        }

        healthBarFilled.fillAmount = hp_current / hp;
    }
}

