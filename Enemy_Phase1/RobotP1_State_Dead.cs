using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP1_State_Dead : Robot_State<Robot_P1>
{
    public void OnEnter(Robot_P1 robot_p1)
    {
        robot_p1.dead = true;
        robot_p1.StartCoroutine(DeadCoroutine(robot_p1));
    }

    public void OnUpdate(Robot_P1 robot_p1)
    {
       // robot_p1.robotAi.AnimationEndCheck();
    }

    public void OnExit(Robot_P1 robot_p1)
    {

    }

    public void OnFixedUpdate(Robot_P1 robot_p1)
    {

    }

    IEnumerator DeadCoroutine(Robot_P1 robot_p1)
    {
        robot_p1.p1_id = "dead";
        robot_p1.Phase1_Animator.SetTrigger("dead");
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.9f);
        robot_p1.ChangeState(Robot_P1.RobotP1_State.BORN);
  
        //robot_p1.gameObject.SetActive(false);
        //ObjectPoolingManager.Instance.GetObject("Phase2", robot_p1.Phase2);
    }
}
