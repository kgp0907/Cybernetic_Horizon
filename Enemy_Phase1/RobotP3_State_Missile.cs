using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP3_State_Missile : Robot_State<Robot_P1>
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
        robot_p1.p1_id = "missile";
        robot_p1.Robot_Animator.SetTrigger("missile");
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.18f);
        ShakeCamera.instance.OnShakeCamera(0.15f, 0.15f);
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.35f);
        for(int i = 0; i < 4; i++)
        {
            ObjectPoolingManager.Instance.GetObject_Noparent("Missile", robot_p1.MissilePos[i]);
        } 
    }
}
