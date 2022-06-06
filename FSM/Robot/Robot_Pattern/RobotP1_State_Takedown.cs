using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP1_State_Takedown : Base_Interface<Robot_Base>
{
    private readonly string ShockwaveTag = "Shockwave";

    public void OnEnter(Robot_Base robot_p1)
    {
        robot_p1.StartCoroutine(TakedownAtk(robot_p1));
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
   
    IEnumerator TakedownAtk(Robot_Base robot_p1)
    {
        robot_p1.Animation_id = "takedown";
        robot_p1.robot_Animator.SetTrigger(robot_p1.Animation_id);

        yield return StaticCoroutine.WaitUntil(robot_p1.Animation_id, robot_p1.robot_Animator, 0.48f);
        CinemachineImpulse.Instance.CameraShake(3f);
        GameObject shockwave= ObjectPoolingManager.Instance.GetObject_Noparent(ShockwaveTag, robot_p1.effectPos_Shockwave);
        robot_p1.RobotP1.colision_P1_RightArm.SetActive(true);
        robot_p1.RobotP1.colision_P1_LeftArm.SetActive(true);

        yield return StaticCoroutine.WaitUntil(robot_p1.Animation_id, robot_p1.robot_Animator, 0.53f);
        robot_p1.RobotP1.colision_P1_RightArm.SetActive(false);
        robot_p1.RobotP1.colision_P1_LeftArm.SetActive(false);

        yield return StaticCoroutine.WaitUntil(robot_p1.Animation_id, robot_p1.robot_Animator, 0.8f);
        ObjectPoolingManager.Instance.ReturnObject(ShockwaveTag, shockwave);
    }

}
