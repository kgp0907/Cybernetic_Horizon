using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP3_State_Missile : Robot_State<Robot_Base>
{
    public void OnEnter(Robot_Base robot_p1)
    {
        robot_p1.StartCoroutine(Missile_Launch(robot_p1));
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

    IEnumerator Missile_Launch(Robot_Base robot_p1)
    {
        robot_p1.Animation_id = "missile";
        robot_p1.Robot_Animator.SetTrigger("missile");
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.18f);
        CinemachineImpulse.Instance.CameraShake(3f);
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.35f);
        for(int i = 0; i < 4; i++)
        {
            ObjectPoolingManager.Instance.GetObject_Noparent("Missile", robot_p1.RobotP3.MissilePos[i]);
        } 
    }
}
