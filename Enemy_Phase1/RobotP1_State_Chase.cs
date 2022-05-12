using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP1_State_Chase : Robot_State<Robot_P1>
{
    public void OnEnter(Robot_P1 robot_p1)
    {
       robot_p1.StartMove();
    }

    public void OnUpdate(Robot_P1 robot_p1)
    {
        Debug.Log("ยัดยม฿");
        float Distance = (robot_p1.target.position - robot_p1.transform.position).magnitude;

       // robot_p1.Phase1_Animator.SetFloat("MoveSpeed", robot_p1.navMeshAgent.velocity.magnitude);
      // robot_p1.Phase2_Animator.SetFloat("MoveSpeed", robot_p1.navMeshAgent.velocity.magnitude);
        if (Distance <= robot_p1.AttackRange &&
                        robot_p1.Attacking == false)
        {
            robot_p1.ChangeState(Robot_P1.RobotP1_State.READY);
        }
        else
        {
            robot_p1.navMeshAgent.SetDestination(robot_p1.target.transform.position);
        }
    }

    public void OnExit(Robot_P1 robot_p1)
    {
     robot_p1.StopMove();
    }

    public void OnFixedUpdate(Robot_P1 robot_p1)
    {

    }

}
