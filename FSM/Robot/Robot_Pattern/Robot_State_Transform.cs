using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_State_Transform : Base_Interface<Robot_Base>
{

    public void OnEnter(Robot_Base robot_p1)
    {
   
        if (robot_p1.isPhase1)
        {
            robot_p1.StartCoroutine(Transform_Phase2(robot_p1));
        }
       

        else if (robot_p1.isPhase2)
        {
            robot_p1.StartCoroutine(Transform_Phase3(robot_p1));
        }
            
    }

    public void OnUpdate(Robot_Base robot_p1)
    {
        robot_p1.robotAi.AnimationEndCheck();
    }

    public void OnExit(Robot_Base robot_p1)
    {

    }

    public void OnFixedUpdate(Robot_Base robot_p1)
    {

    }
    IEnumerator Transform_Phase2(Robot_Base robot_p1)
    {
        robot_p1.Animation_id = "transform_2";
        robot_p1.robot_Animator.SetTrigger(robot_p1.Animation_id);
        robot_p1.next_TransformObject.SetActive(true);
        robot_p1.next_TransformObject.transform.position = robot_p1.gameObject.transform.position;
        robot_p1.next_TransformObject.transform.rotation = robot_p1.gameObject.transform.rotation;
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.75f);
        robot_p1.gameObject.SetActive(false);
    }
    IEnumerator Transform_Phase3(Robot_Base robot_p1)
    {
        robot_p1.Animation_id = "transform_3";
        robot_p1.robot_Animator.SetTrigger(robot_p1.Animation_id);
        robot_p1.next_TransformObject.SetActive(true);
        robot_p1.next_TransformObject.transform.position = robot_p1.gameObject.transform.position;
        robot_p1.next_TransformObject.transform.rotation = robot_p1.gameObject.transform.rotation;
        robot_p1.gameObject.SetActive(false);
        yield return null;

    }
}
