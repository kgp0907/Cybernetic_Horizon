using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP3_State_Claw : Robot_State<Robot_P1>
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
        robot_p1.p1_id = "claw";
        robot_p1.Robot_Animator.SetTrigger("claw");
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.55f);
        robot_p1.RobotP3.Colision_P3_RightArm.SetActive(true);
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.63f);
        robot_p1.RobotP3.Colision_P3_RightArm.SetActive(false);
        robot_p1.RobotP3.Colision_P3_LeftArm.SetActive(true);
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.68f);
        robot_p1.RobotP3.Colision_P3_LeftArm.SetActive(false);
    }
}
