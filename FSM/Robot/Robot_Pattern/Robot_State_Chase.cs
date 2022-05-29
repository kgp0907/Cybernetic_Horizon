using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �κ��� �÷��̾� ������ ����ϴ� ������Ʈ
/// ������ ������ �ش� ������Ʈ�� ��ȯ�ȴ�.
/// </summary>
public class Robot_State_Chase : Base_Interface<Robot_Base>
{

    public void OnEnter(Robot_Base robot)
    {
        robot.StartMove();
    }

    //���ݹ����� ������ ������ �غ�, �ƴϸ� ��� �߰��Ѵ�.
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

