using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP3_State_ClawAtk : Base_Interface<Robot_Base>
{
    public void OnEnter(Robot_Base robot_p1)
    {
        robot_p1.StartCoroutine(ClawAtk(robot_p1));
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

    IEnumerator ClawAtk(Robot_Base robot_p1)
    {
        robot_p1.Animation_id = "claw";
        robot_p1.robot_Animator.SetTrigger(robot_p1.Animation_id);
        //  yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.55f);
        yield return StaticCoroutine.WaitUntil(robot_p1.Animation_id, 0.55f, robot_p1.robot_Animator);
        robot_p1.RobotP3.colision_P3_RightArm.SetActive(true);
        //  yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.63f);
        yield return StaticCoroutine.WaitUntil(robot_p1.Animation_id, 0.63f, robot_p1.robot_Animator);
        robot_p1.RobotP3.colision_P3_RightArm.SetActive(false);
        robot_p1.RobotP3.colision_P3_LeftArm.SetActive(true);
        //   yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.68f);
        yield return StaticCoroutine.WaitUntil(robot_p1.Animation_id, 0.68f, robot_p1.robot_Animator);
        robot_p1.RobotP3.colision_P3_LeftArm.SetActive(false);
    }
}
