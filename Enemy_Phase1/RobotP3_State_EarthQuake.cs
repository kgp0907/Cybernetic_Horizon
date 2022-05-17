using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP3_State_EarthQuake : Robot_State<Robot_P1>
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
        robot_p1.p1_id = "earth";
        robot_p1.Robot_Animator.SetTrigger("earth");
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.39f);
        ShakeCamera.instance.OnShakeCamera(0.3f, 0.3f);
        GameObject shockwave = ObjectPoolingManager.Instance.GetObject_Noparent("Shockwave", robot_p1.ImpactEffectPos);
        robot_p1.RobotP3.Colision_P3_RightLeg.SetActive(true);
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.45f);
        robot_p1.RobotP3.Colision_P3_RightLeg.SetActive(false);
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.9f);
        ObjectPoolingManager.Instance.ReturnObject("Shockwave", shockwave);
    }
}
