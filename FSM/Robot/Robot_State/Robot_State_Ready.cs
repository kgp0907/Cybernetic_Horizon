using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_State_Ready : Interface_Base<Robot_Base>
{
    //   private Robot_P1_Pattern robotP1_Pattern;
    public void OnEnter(Robot_Base robot_p1)
    {
        robot_p1.StopMove();
        robot_p1.StartCoroutine(AtkReadyCoroutine(robot_p1));
    }

    public void OnExit(Robot_Base robot_p1)
    {

    }

    public void OnFixedUpdate(Robot_Base robot_p1)
    {

    }

    public void OnUpdate(Robot_Base robot_p1)
    {

    }

    public void AtkReady(Robot_Base robot)
    {

    }

    public IEnumerator AtkReadyCoroutine(Robot_Base robot_p1)
    {
        robot_p1.attacking = true;

        while (robot_p1.ActReadyTime >= 0f)
        {
            robot_p1.ActReadyTime -= Time.deltaTime;
            Rotation(robot_p1);
            yield return null;
        }

        if (robot_p1.rangedMode)
        {
            robot_p1.RobotP3.Phase3_RangeAtk(robot_p1);
            robot_p1.rangedMode = false;
        }
        else
        {
            robot_p1.robotP1_Pattern.NextState(robot_p1);
        }

        robot_p1.ActReadyTime = robot_p1.actReadyTime;
    }

    public void Rotation(Robot_Base robot_p1)
    {
        var targetPos = robot_p1.target.position;
        targetPos.y = robot_p1.transform.position.y;
        var targetDir = Quaternion.LookRotation(targetPos - robot_p1.transform.position);
        robot_p1.transform.rotation = Quaternion.Slerp(robot_p1.transform.rotation, targetDir,
                                                     robot_p1.RotationSpeed * Time.deltaTime);
    }


}
