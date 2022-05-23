using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP2_State_Punch : Robot_State<Robot_Base>
{
    public void OnEnter(Robot_Base robot_p1)
    {
        robot_p1.StartCoroutine(AttackClap(robot_p1));
    }

    public void OnUpdate(Robot_Base robot_p1)
    {
        robot_p1.robotAi.AnimationEndCheck();

    }

    public void OnExit(Robot_Base robot_p1)
    {

    }

    public void OnFixedUpdate(Robot_Base robot_p1)
    {

    }

    IEnumerator AttackClap(Robot_Base robot_p1)
    {
        robot_p1.Animation_id = "punch";
        robot_p1.Robot_Animator.SetTrigger("punch");
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.35f);
        robot_p1.RobotP2.Colision_P2_RightArm.SetActive(true);
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.4f);
        robot_p1.RobotP2.Colision_P2_RightArm.SetActive(false);

    }
}
