using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_AI : MonoBehaviour
{
    Robot_Base robot;
    Enemy_Pattern robotPattern;
    private void Start()
    {
        robotPattern = this.transform.GetComponent<Enemy_Pattern>();
        robot = this.transform.GetComponent<Robot_Base>();
        robot.ChangeState(Robot_Base.RobotP1_State.BORN);
    }
    
    private void Update()
    {
        if (robot.isPhase3 == true)
        {
            SwitchRangeMode();
        }
    }

    public void AnimationEndCheck()
    {
        if (robot.AnimationName && robot.AnimationProgress >= 0.9f)
        {

            robot.ChangeState(Robot_Base.RobotP1_State.CHASE);
          
            robot.attacking = false;
        }
    }


    public void SwitchRangeMode()
    { 
            float distance = (robot.target.position - robot.transform.position).sqrMagnitude;

        if (distance >= robot.SightRange * robot.SightRange && robot.CurState(Robot_P1.RobotP1_State.CHASE) && !robot.rangedMode)
        {
            robot.rangedMode = true;
            robot.ChangeState(Robot_Base.RobotP1_State.READY);
        }
    }
    
}
