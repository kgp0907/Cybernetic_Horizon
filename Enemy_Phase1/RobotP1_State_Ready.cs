using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotP1_State_Ready : Robot_State<Robot_P1>
{
 //   private Robot_P1_Pattern robotP1_Pattern;
    Animator enemyani;
    public void OnEnter(Robot_P1 robot_p1)
    {
        robot_p1.StopMove();
        robot_p1.StartCoroutine(AtkReadyCoroutine(robot_p1));
    }

    public void OnExit(Robot_P1 robot_p1)
    {

    }

    public void OnFixedUpdate(Robot_P1 robot_p1)
    {

    }

    public void OnUpdate(Robot_P1 robot_p1)
    {
      //  robot_p1.StopMove();
    }
    public IEnumerator AtkReadyCoroutine(Robot_P1 robot_p1)
    {
        int rand = Random.Range(0, 3);
        robot_p1.Attacking = true;
       
        while (robot_p1.ActReadyTime >= 0f)
        {
            robot_p1.ActReadyTime -= Time.deltaTime;
            Rotation(robot_p1);
            yield return null;
        }

        if (robot_p1.RobotP3.RangedMode && rand!=2)
            Phase3_RangeAtk(robot_p1);
     
        else
            robot_p1.robotP1_Pattern.NextState(robot_p1);

        robot_p1.ActReadyTime = robot_p1.actReadyTime;
    }
    public void Rotation(Robot_P1 robot_p1)
    {
        var targetPos = robot_p1.target.position;
        targetPos.y = robot_p1.transform.position.y;
        var targetDir = Quaternion.LookRotation(targetPos - robot_p1.transform.position);
        robot_p1.transform.rotation = Quaternion.Slerp(robot_p1.transform.rotation, targetDir,
                                                     robot_p1.RotationSpeed * Time.deltaTime);
    }

    public void Phase3_RangeAtk(Robot_P1 robot_p1)
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            robot_p1.ChangeState(Robot_P1.RobotP1_State.P3_ATTACK_HOMING_MISSILE);
        }
        else
        {
            robot_p1.ChangeState(Robot_P1.RobotP1_State.P3_ATTACK_BOMB);
        }
        robot_p1.RobotP3.RangedMode = false;
    }
}
