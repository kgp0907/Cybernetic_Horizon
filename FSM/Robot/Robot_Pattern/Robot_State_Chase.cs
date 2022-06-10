using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 로봇의 플레이어 추적을 담당하는 스테이트
/// 공격이 끝나면 해당 스테이트로 전환된다.
/// </summary>
public class Robot_State_Chase : Interface_Base<Robot_Base>
{
    private bool isTarget = false;
    private float currentDist = 0;      //현재 거리
    private float closetDist = 100f;    //가까운 거리
    private float targetDist = 100f;   //타겟 거리
    private int closeDistIndex = 0;    //가장 가까운 인덱스
    private int targetIndex = -1;      //타겟팅 할 인덱스

    public void OnEnter(Robot_Base robot)
    {
        robot.isChasing = true;
        robot.StartMove();
    }

    //공격범위에 들어오면 공격을 준비, 아니면 계속 추격한다.
    public void OnUpdate(Robot_Base robot)
    {
        if (robot.target == null)
        {
            return;
        }

        float distance = (robot.target.position - robot.transform.position).sqrMagnitude;

        if (distance <= robot.attackRange * robot.attackRange &&
                        robot.attacking == false)
        {
            robot.ChangeState(Robot_Base.Robot_State.READY);
        }
        else
        {
            robot.navMeshAgent.SetDestination(robot.target.transform.position);
        }
    }

    public void OnExit(Robot_Base robot)
    {
        robot.isChasing = false;
        robot.StopMove();
    }

    public void OnFixedUpdate(Robot_Base robot)
    {


    }
}