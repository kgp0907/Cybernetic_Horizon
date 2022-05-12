using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP2_State_Smash : Robot_State<Robot_P1>
{
    public void OnEnter(Robot_P1 robot_p1)
    {
        robot_p1.StartCoroutine(AttackClap(robot_p1));
    }

    public void OnUpdate(Robot_P1 robot_p1)
    {
        robot_p1.robotAi.AnimationEndCheck();

    }

    public void OnExit(Robot_P1 robot_p1)
    {

    }

    public void OnFixedUpdate(Robot_P1 robot_p1)
    {

    }

    IEnumerator AttackClap(Robot_P1 robot_p1)
    {
        robot_p1.p1_id = "smash";
        robot_p1.Phase1_Animator.SetTrigger("smash");
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.9f);
    }
}
