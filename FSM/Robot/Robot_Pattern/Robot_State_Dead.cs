using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_State_Dead : Base_Interface<Robot_Base>
{
    public void OnEnter(Robot_Base robot_p1)
    {
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
        robot_p1.robot_Animator.SetTrigger(robot_p1.Animation_id);

        if (robot_p1.isPhase1)
        {
            yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.8f);
        }

        if (robot_p1.isPhase3)
            yield break;
        robot_p1.ChangeState(Robot_Base.RobotP1_State.TRANFORM);
    }


}

