using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_State_Born : Base_Interface<Robot_Base>
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
}
