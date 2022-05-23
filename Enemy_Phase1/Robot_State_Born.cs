using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_State_Born : Robot_State<Robot_Base>
{
    

    public void OnEnter(Robot_Base robot_p1)
    {
        robot_p1.Animation_id = "Born";
  
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

    IEnumerator BornCoroutine(Robot_Base robot_p1)
    {
       
        yield return new WaitUntil(() => robot_p1.AnimationName && robot_p1.AnimationProgress >= 0.9f);
    
        //ShakeCamera.instance.OnShakeCamera(0.2f, 0.2f);
    }
}
