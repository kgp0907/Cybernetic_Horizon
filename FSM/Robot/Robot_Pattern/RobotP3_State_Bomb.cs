using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP3_State_Bomb : Interface_Base<Robot_Base>
{
    private GameObject bomb1_Obj;
    private GameObject bomb2_Obj;
    private GameObject bomb3_Obj;
    private GameObject fireExplosion_Obj;
    private GameObject fireExplosion2_Obj;
    private string fireExplosionEffect = "fireExplosionEffect";
    private string bombEffect = "bombEffect";

    public void OnEnter(Robot_Base robot_p1)
    {
        robot_p1.StartCoroutine(Fire_Cannon(robot_p1));
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

    IEnumerator Fire_Cannon(Robot_Base robot_p1)
    {
        robot_p1.animation_id = "bomb";
        robot_p1.m_Animator.SetTrigger(robot_p1.animation_id);
        yield return StaticCoroutine.WaitUntil(robot_p1.animation_id, robot_p1.m_Animator, 0.12f);
        CinemachineImpulse.Instance.CameraShake(3f);

        yield return StaticCoroutine.WaitUntil(robot_p1.animation_id, robot_p1.m_Animator, 0.35f);
        CinemachineImpulse.Instance.CameraShake(3f);

        yield return StaticCoroutine.WaitUntil(robot_p1.animation_id, robot_p1.m_Animator, 0.57f);
        CinemachineImpulse.Instance.CameraShake(3f);
        fireExplosion_Obj = ObjectPoolingManager.Instance.GetObject(fireExplosionEffect, robot_p1.RobotP3.MissilePos[4]);
        fireExplosion2_Obj = ObjectPoolingManager.Instance.GetObject(fireExplosionEffect, robot_p1.RobotP3.MissilePos[5]);
        bomb1_Obj = ObjectPoolingManager.Instance.GetObject_Noparent(bombEffect, robot_p1.RobotP3.BombRespawnPos[0]);

        yield return StaticCoroutine.WaitUntil(robot_p1.animation_id, robot_p1.m_Animator, 0.62f);
        CinemachineImpulse.Instance.CameraShake(3f);
        bomb2_Obj = ObjectPoolingManager.Instance.GetObject_Noparent(bombEffect, robot_p1.RobotP3.BombRespawnPos[1]);
        yield return StaticCoroutine.WaitUntil(robot_p1.animation_id, robot_p1.m_Animator, 0.67f);
        CinemachineImpulse.Instance.CameraShake(3f);
        bomb3_Obj = ObjectPoolingManager.Instance.GetObject_Noparent(bombEffect, robot_p1.RobotP3.BombRespawnPos[2]);
        yield return StaticCoroutine.WaitUntil(robot_p1.animation_id, robot_p1.m_Animator, 0.72f);
        ObjectPoolingManager.Instance.ReturnObject(fireExplosionEffect, fireExplosion_Obj);
        ObjectPoolingManager.Instance.ReturnObject(fireExplosionEffect, fireExplosion2_Obj);
    }
}