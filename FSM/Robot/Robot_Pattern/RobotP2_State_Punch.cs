using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP2_State_Punch : Base_Interface<Robot_Base>
{
    private WaitUntil waitforinformation;


    public void OnEnter(Robot_Base robot_p1)
    {
        robot_p1.StartCoroutine(PunchAtk(robot_p1));
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

    IEnumerator PunchAtk(Robot_Base robot_p1)
    {
        robot_p1.Animation_id = "punch";
        robot_p1.robot_Animator.SetTrigger(robot_p1.Animation_id);
        //   yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.35f);
        yield return StaticCoroutine.WaitUntil(robot_p1.Animation_id, 0.35f, robot_p1.robot_Animator);
        robot_p1.RobotP2.colision_P2_RightArm.SetActive(true);
        yield return StaticCoroutine.WaitUntil(robot_p1.Animation_id, 0.4f, robot_p1.robot_Animator);
        //  yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.4f);
        robot_p1.RobotP2.colision_P2_RightArm.SetActive(false);

    }
}
