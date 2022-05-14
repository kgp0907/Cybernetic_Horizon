using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_Takedamage : MonoBehaviour
{
    Robot_P1 robotP1;
    public float eHp_Current = 200;
    public float EffectReturnTime = 2f;
    public void Awake()
    {
        robotP1 = Robot_P1.FindObjectOfType<Robot_P1>();
    }
    public void TakeDamage(float damage)
    {
        eHp_Current -= damage;
       // StartCoroutine(AttackEffect());
    }
    private void Update()
    {
        if (eHp_Current <= 0f && !robotP1.dead)
        {
            robotP1.ChangeState(Robot_P1.RobotP1_State.DEAD);
        }
        //else if (eHp_Current <= 50f && !robotP1.Stunned)
        //{
        //    robotP1.Stunned = true;
        //    robotP1.ChangeState(robotP1.EnemyState.STUN);
        //}
    }

    //public IEnumerator AttackEffect()
    //{
    //    //GameObject explosion = ObjectPoolingManager.Instance.GetObject_Noparent("Explosion", dragon.HitEffect);
    //    yield return new WaitForSeconds(EffectReturnTime);
    //    ObjectPoolingManager.Instance.ReturnObject("Explosion", explosion);
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("DeadAtk"))
    //    {
    //        eHp_Current = 10;
    //    }
    //}
}
