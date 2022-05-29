using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_P3 : Robot_Base
{

    public GameObject[] BombRespawnPos;
    public GameObject[] MissilePos;

    public GameObject colision_P3_RightLeg;
    public GameObject colision_P3_RightArm;
    public GameObject colision_P3_LeftArm;

    public void Phase3_RangeAtk(Robot_Base robot_p1)
    {
        if (robot_p1.isPhase3==true)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                robot_p1.ChangeState(Robot_Base.RobotP1_State.P3_ATTACK_HOMING_MISSILE);
            }
            else
            {
                robot_p1.ChangeState(Robot_Base.RobotP1_State.P3_ATTACK_BOMB);
            }
            robot_p1.rangedMode = false;
        }
       
    }
}
