using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP1_State_Born : Robot_State<Robot_P1>
{


    public void OnEnter(Robot_P1 robot_p1)
    {
        if (robot_p1.dead)
        {
            robot_p1.StartCoroutine(Phase2SwitchCoroutine(robot_p1));
        }
        else
        {
            robot_p1.StartCoroutine(BornCoroutine(robot_p1));
        }
      
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

    IEnumerator BornCoroutine(Robot_P1 robot_p1)
    {
        robot_p1.p1_id = "P1_Born";
        robot_p1.Phase1_Animator.SetTrigger("P1_Born");
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.9f);

    }

    IEnumerator Phase2SwitchCoroutine(Robot_P1 robot_p1)
    {
        robot_p1.p1_id = "2_Born";
        robot_p1.Phase1_Animator.SetTrigger("2_Born");
        //  ObjectPoolingManager.Instance.GetObject_Noparent("Phase2", robot_p1.gameObject);
        robot_p1.Phase2.SetActive(true);
        robot_p1.Phase2.transform.position = robot_p1.gameObject.transform.position;
        robot_p1.Phase2.transform.rotation = robot_p1.gameObject.transform.rotation;
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.9f);

        robot_p1.gameObject.SetActive(false);
        
    }
}
