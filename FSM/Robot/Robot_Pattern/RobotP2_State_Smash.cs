using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP2_State_Smash : Interface_Base<Robot_Base>
{
    private readonly string ShockwaveTag = "Shockwave";
    private readonly string Phase2_Smash_Effect = "Phase2_Smash_Effect";

    public void OnEnter(Robot_Base robot_p1)
    {
        robot_p1.StartCoroutine(SmashAtk(robot_p1));
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

    IEnumerator SmashAtk(Robot_Base robot_p1)
    {
        robot_p1.animation_id = "smash";
        robot_p1.m_Animator.SetTrigger(robot_p1.animation_id);

        yield return StaticCoroutine.WaitUntil(robot_p1.animation_id, robot_p1.m_Animator, 0.37f);

        GameObject shockwave = ObjectPoolingManager.Instance.GetObject_Noparent(ShockwaveTag, robot_p1.effectPos_Shockwave);
        GameObject smashEffect = ObjectPoolingManager.Instance.GetObject_Noparent(Phase2_Smash_Effect, robot_p1.effectPos_Shockwave);
        robot_p1.RobotP2.colision_P2_LeftArm.SetActive(true);
        CinemachineImpulse.Instance.CameraShake(3f);

        yield return StaticCoroutine.WaitUntil(robot_p1.animation_id, robot_p1.m_Animator, 0.42f);

        robot_p1.RobotP2.colision_P2_LeftArm.SetActive(false);
        yield return StaticCoroutine.WaitUntil(robot_p1.animation_id, robot_p1.m_Animator, 0.8f);
        ObjectPoolingManager.Instance.ReturnObject(ShockwaveTag, shockwave);
        ObjectPoolingManager.Instance.ReturnObject(Phase2_Smash_Effect, smashEffect);
    }
}
