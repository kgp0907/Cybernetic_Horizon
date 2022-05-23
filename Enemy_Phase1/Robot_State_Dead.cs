using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_State_Dead : Robot_State<Robot_Base>
{
    public void OnEnter(Robot_Base robot_p1)
    {
        Debug.Log("dd");
       // robot_p1.StopAllCoroutines();
       robot_p1.StartCoroutine(TransForm_Phase(robot_p1));
    }

    public void OnUpdate(Robot_Base robot_p1)
    {
 
    }

    public void OnExit(Robot_Base robot_p1)
    {

    }

    public void OnFixedUpdate(Robot_Base robot_p1)
    {

    }

    IEnumerator TransForm_Phase(Robot_Base robot_p1)
    {
      
        robot_p1.Animation_id = "dead";
        robot_p1.Robot_Animator.SetTrigger("dead");

        if (robot_p1.isPhase3)
        {
            yield return null;
        }
        else if (robot_p1.isPhase2)
        {
            robot_p1.RobotP3.isPhase3 = true;
            robot_p1.ChangeState(Robot_Base.RobotP1_State.TRANFORM);
        }
        else if (robot_p1.isPhase1)
        {
            yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.8f);
            robot_p1.RobotP2.isPhase2 = true;
            robot_p1.ChangeState(Robot_Base.RobotP1_State.TRANFORM);
        }
        
       
    }


}

