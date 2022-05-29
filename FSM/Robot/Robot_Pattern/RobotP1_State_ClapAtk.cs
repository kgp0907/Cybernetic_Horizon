using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP1_State_ClapAtk : Base_Interface<Robot_Base>
{
    public void OnEnter(Robot_Base robot_p1)
    {
        robot_p1.StartCoroutine(ClapAtk(robot_p1));
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


    IEnumerator ClapAtk(Robot_Base robot_p1)
    {
        robot_p1.Animation_id = "clap";
        robot_p1.robot_Animator.SetTrigger(robot_p1.Animation_id);
        //   yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.53f);
        yield return StaticCoroutine.WaitUntil(robot_p1.Animation_id, 0.53f, robot_p1.robot_Animator);
        robot_p1.RobotP1.colision_P1_RightArm.SetActive(true);
        robot_p1.RobotP1.colision_P1_LeftArm.SetActive(true);
        yield return StaticCoroutine.WaitUntil(robot_p1.Animation_id, 0.58f, robot_p1.robot_Animator);
       // yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.58f);
        robot_p1.RobotP1.colision_P1_RightArm.SetActive(false);
        robot_p1.RobotP1.colision_P1_LeftArm.SetActive(false);
    }

}
