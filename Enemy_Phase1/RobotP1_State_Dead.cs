using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP1_State_Dead : Robot_State<Robot_P1>
{
    public void OnEnter(Robot_P1 robot_p1)
    {
        Debug.Log("»ç¸Á");
        robot_p1.dead = true;
        if(robot_p1.phase2)
        robot_p1.StartCoroutine(TransForm_Phase3(robot_p1));
        else
        {
            robot_p1.StartCoroutine(TransForm_Phase2(robot_p1));
        }
    }

    public void OnUpdate(Robot_P1 robot_p1)
    {
 
    }

    public void OnExit(Robot_P1 robot_p1)
    {

    }

    public void OnFixedUpdate(Robot_P1 robot_p1)
    {

    }

    IEnumerator TransForm_Phase2(Robot_P1 robot_p1)
    {
        robot_p1.p1_id = "dead";
        robot_p1.Phase1_Animator.SetTrigger("dead");
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.9f);
        robot_p1.phase2 = true;
        robot_p1.ChangeState(Robot_P1.RobotP1_State.BORN);
    }

    IEnumerator TransForm_Phase3(Robot_P1 robot_p1)
    {
        Debug.Log("3º¯½Å");
        robot_p1.p1_id = "3_Born";
        robot_p1.Phase1_Animator.SetTrigger("3_Born"); 
        robot_p1.phase3 = true;
        robot_p1.ChangeState(Robot_P1.RobotP1_State.BORN);
        yield return null;
    
    }
}
