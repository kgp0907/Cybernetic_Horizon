using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP3_State_Bomb : Robot_State<Robot_P1>
{
    Vector3 vec = new Vector3(0, 0, 15);
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
        robot_p1.p1_id = "bomb";
        robot_p1.Robot_Animator.SetTrigger("bomb");
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.12f);
        ShakeCamera.instance.OnShakeCamera(0.15f, 0.15f);
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.35f);
        ShakeCamera.instance.OnShakeCamera(0.15f, 0.15f);
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.57f);
        ShakeCamera.instance.OnShakeCamera(0.3f, 0.3f);
        GameObject bomb1 = ObjectPoolingManager.Instance.GetObject_Noparent("bomb", robot_p1.RobotP3.BombRespawnPos[0]);
        robot_p1.StartCoroutine(ReturnCoroutine(bomb1));
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.62f);
        ShakeCamera.instance.OnShakeCamera(0.3f, 0.3f);
        GameObject bomb2= ObjectPoolingManager.Instance.GetObject_Noparent("bomb", robot_p1.RobotP3.BombRespawnPos[1]);
        robot_p1.StartCoroutine(ReturnCoroutine(bomb2));
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.67f);
        ShakeCamera.instance.OnShakeCamera(0.3f, 0.3f);
        GameObject bomb3 = ObjectPoolingManager.Instance.GetObject_Noparent("bomb", robot_p1.RobotP3.BombRespawnPos[2]);
        robot_p1.StartCoroutine(ReturnCoroutine(bomb3));
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.72f);
 
    }
    IEnumerator ReturnCoroutine(GameObject bomb)
    {
        yield return new WaitForSeconds(2f);
        ObjectPoolingManager.Instance.ReturnObject("bomb", bomb);
    }
}