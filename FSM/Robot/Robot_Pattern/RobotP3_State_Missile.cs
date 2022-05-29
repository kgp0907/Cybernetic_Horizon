using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP3_State_Missile : Base_Interface<Robot_Base>
{
    private readonly string Missile = "Missile";

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
        robot_p1.robot_Animator.SetTrigger(robot_p1.Animation_id);
        //    yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.18f);
        yield return StaticCoroutine.WaitUntil(robot_p1.Animation_id, 0.18f, robot_p1.robot_Animator);
        CinemachineImpulse.Instance.CameraShake(3f);
        //  yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.35f);
        yield return StaticCoroutine.WaitUntil(robot_p1.Animation_id, 0.35f, robot_p1.robot_Animator);
        for (int i = 0; i < 4; i++)
        {
            ObjectPoolingManager.Instance.GetObject_Noparent(Missile, robot_p1.RobotP3.MissilePos[i]);
        } 
    }
}
