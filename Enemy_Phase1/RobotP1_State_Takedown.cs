using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP1_State_Takedown : Robot_State<Robot_P1>
{
    public void OnEnter(Robot_P1 robot_p1)
    {
        robot_p1.StartCoroutine(AttackTakedown(robot_p1));
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


   
    IEnumerator AttackTakedown(Robot_P1 robot_p1)
    {
        robot_p1.p1_id = "takedown";
        robot_p1.Robot_Animator.SetTrigger("takedown");
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.48f);
        ShakeCamera.instance.OnShakeCamera(0.4f, 0.4f);
        GameObject shockwave= ObjectPoolingManager.Instance.GetObject_Noparent("Shockwave", robot_p1.ImpactEffectPos);
        robot_p1.Colision_P1_RightArm.SetActive(true);
        robot_p1.Colision_P1_LeftArm.SetActive(true);
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.53f);
        robot_p1.Colision_P1_RightArm.SetActive(false);
        robot_p1.Colision_P1_LeftArm.SetActive(false);
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.8f);
        ObjectPoolingManager.Instance.ReturnObject("Shockwave", shockwave);
    }

}
