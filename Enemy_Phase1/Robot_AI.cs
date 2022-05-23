using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_AI : MonoBehaviour
{
    Robot_Base robotP1;
    Robot_P1_Pattern robotp1_Pattern;
    private void Start()
    {
        robotp1_Pattern = this.transform.GetComponent<Robot_P1_Pattern>();
        robotP1 = this.transform.GetComponent<Robot_Base>();
        robotP1.ChangeState(Robot_Base.RobotP1_State.BORN);
    }
    
    private void Update()
    {

        if (robotP1.isPhase3)
        {
            float distance = (robotP1.target.position - robotP1.transform.position).sqrMagnitude;
            if (distance >= robotP1.SightRange * robotP1.SightRange && robotP1.IsState(Robot_P1.RobotP1_State.CHASE)&&!robotP1.RangedMode)
            {
                robotP1.RangedMode = true;
                robotP1.ChangeState(Robot_Base.RobotP1_State.READY);
            }
           
        }
    }
    public void AnimationEndCheck()
    {
        if (robotP1.AnimationName && robotP1.AnimationProgress >= 0.9f)
        {

            robotP1.ChangeState(Robot_Base.RobotP1_State.CHASE);
          
            robotP1.Attacking = false;
        }
    }

    
}
