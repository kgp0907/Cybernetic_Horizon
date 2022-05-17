using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP1_State_Chase : Robot_State<Robot_P1>
{
    public void OnEnter(Robot_P1 robot_p1)
    {
        //if (robot_p1.RobotP3.phase3 == true)
        //{
        //    Debug.Log(robot_p1.RobotP3.phase3);
        //    Phase3_RangeAtk(robot_p1);
        //}
        //else
            robot_p1.StartMove();
        
      
    }

    public void OnUpdate(Robot_P1 robot_p1)
    {

        float Distance = (robot_p1.target.position - robot_p1.transform.position).magnitude;

       
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

   
    public void Phase3_RangeAtk(Robot_P1 robot_p1)
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            robot_p1.ChangeState(Robot_P1.RobotP1_State.P3_ATTACK_HOMING_MISSILE);
        }
        else
        {
            robot_p1.ChangeState(Robot_P1.RobotP1_State.P3_ATTACK_BOMB);
        }
        robot_p1.RobotP3.RangedMode = false;
    }
}
