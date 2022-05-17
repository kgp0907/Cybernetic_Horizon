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
        robot_p1.Robot_Animator.SetTrigger("smash");
    
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.37f);
        GameObject shockwave= ObjectPoolingManager.Instance.GetObject_Noparent("Shockwave", robot_p1.ImpactEffectPos);
        GameObject ex06= ObjectPoolingManager.Instance.GetObject_Noparent("ex06", robot_p1.ImpactEffectPos);
        robot_p1.RobotP2.Colision_P2_LeftArm.SetActive(true);
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.42f);
        robot_p1.RobotP2.Colision_P2_LeftArm.SetActive(false);
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.8f);
        ObjectPoolingManager.Instance.ReturnObject("Shockwave", shockwave);
        ObjectPoolingManager.Instance.ReturnObject("ex06", ex06);
    }
}
