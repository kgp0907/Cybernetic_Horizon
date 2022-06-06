using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP3_State_EarthQuake : Interface_Base<Robot_Base>
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
        yield return StaticCoroutine.WaitUntil(robot_p1.Animation_id, robot_p1.robot_Animator, 0.39f);
        CinemachineImpulse.Instance.CameraShake(3f);
        GameObject shockwave = ObjectPoolingManager.Instance.GetObject_Noparent("Shockwave", robot_p1.effectPos_Shockwave);
        robot_p1.RobotP3.colision_P3_RightLeg.SetActive(true);
        yield return StaticCoroutine.WaitUntil(robot_p1.Animation_id, robot_p1.robot_Animator, 0.45f);
        robot_p1.RobotP3.colision_P3_RightLeg.SetActive(false);
        yield return StaticCoroutine.WaitUntil(robot_p1.Animation_id, robot_p1.robot_Animator, 0.9f);
        ObjectPoolingManager.Instance.ReturnObject("Shockwave", shockwave);
    }
}
