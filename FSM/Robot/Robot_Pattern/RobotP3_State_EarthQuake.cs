using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP3_State_EarthQuake : Base_Interface<Robot_Base>
{
    public void OnEnter(Robot_Base robot_p1)
    {
        robot_p1.StartCoroutine(Attack_Earthquake(robot_p1));
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

    IEnumerator Attack_Earthquake(Robot_Base robot_p1)
    {
        robot_p1.Animation_id = "earth";
        robot_p1.robot_Animator.SetTrigger(robot_p1.Animation_id);
        //  yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.39f);
        yield return StaticCoroutine.WaitUntil(robot_p1.Animation_id, 0.39f, robot_p1.robot_Animator);
        CinemachineImpulse.Instance.CameraShake(3f);
        GameObject shockwave = ObjectPoolingManager.Instance.GetObject_Noparent("Shockwave", robot_p1.effectPos_Shockwave);
        robot_p1.RobotP3.colision_P3_RightLeg.SetActive(true);
        //   yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.45f);
        yield return StaticCoroutine.WaitUntil(robot_p1.Animation_id, 0.45f, robot_p1.robot_Animator);
        robot_p1.RobotP3.colision_P3_RightLeg.SetActive(false);
        //  yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.9f);
        yield return StaticCoroutine.WaitUntil(robot_p1.Animation_id, 0.9f, robot_p1.robot_Animator);
        ObjectPoolingManager.Instance.ReturnObject("Shockwave", shockwave);
    }
}
