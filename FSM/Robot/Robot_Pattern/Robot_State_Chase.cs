using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 로봇의 플레이어 추적을 담당하는 스테이트
/// 공격이 끝나면 해당 스테이트로 전환된다.
/// </summary>
public class Robot_State_Chase : Base_Interface<Robot_Base>
{

    public void OnEnter(Robot_Base robot)
    {
        robot.StartMove();
    }

    //공격범위에 들어오면 공격을 준비, 아니면 계속 추격한다.
    public void OnUpdate(Robot_Base robot)
    {

        float distance = (robot.target.position - robot.transform.position).sqrMagnitude;

        if (distance <= robot.attackRange*robot.attackRange &&
                        robot.attacking == false)
        {
            robot.ChangeState(Robot_Base.RobotP1_State.READY);
        }
        else
        {
            robot.navMeshAgent.SetDestination(robot.target.transform.position);
        }
    }

    public void OnExit(Robot_Base robot)
    {
        robot.StopMove();
    }

    public void OnFixedUpdate(Robot_Base robot)
    {

    }
}

