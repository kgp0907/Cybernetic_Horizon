using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_State_Dead : Interface_Base<Robot_Base>
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
        robot_p1.animation_id = "dead";
        robot_p1.m_Animator.SetTrigger(robot_p1.animation_id);

        if (robot_p1.isPhase1)
        {
            yield return StaticCoroutine.WaitUntil(robot_p1.animation_id, robot_p1.m_Animator, 0.8f);
        }

        if (robot_p1.isPhase3)
            yield break;
        robot_p1.ChangeState(Robot_Base.Robot_State.TRANFORM);
    }


}

