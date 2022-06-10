using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_AI : MonoBehaviour
{
    Player player;
    Robot_Base robot;
    Fsm_Base<Robot_Base> fsm_base;
    Enemy_Pattern robotPattern;

    private bool isTarget = false;
    private float currentDist = 0;      //현재 거리
    private float closetDist = 100f;    //가까운 거리
    private float targetDist = 100f;   //타겟 거리
    private int closeDistIndex = 0;    //가장 가까운 인덱스
    private int targetIndex = -1;      //타겟팅 할 인덱스
    private Collider[] enemies;

    private void Start()
    {
        fsm_base = GetComponent<Fsm_Base<Robot_Base>>();
        player = GetComponent<Player>();
        robotPattern = this.transform.GetComponent<Enemy_Pattern>();
        robot = this.transform.GetComponent<Robot_Base>();
        robot.ChangeState(Robot_Base.Robot_State.BORN);
        enemies = Physics.OverlapSphere(robot.transform.position, robot.SightRange, robot.layerMask);
    }

    private void Update()
    {
        if (robot.target == null)
        {
            UpdateTarget(robot);
        }


        if (robot.isPhase3 == true)
        {
            SwitchRangeMode();
        }
    }


    public void AnimationEndCheck()
    {
        if (robot.AnimationName && robot.AnimationProgress >= 0.9f)
        {
            robot.ChangeState(Robot_Base.Robot_State.CHASE);

            robot.attacking = false;
        }
    }


    void UpdateTarget(Robot_Base robot)
    {
        if (robot.target)
            return;

        enemies = Physics.OverlapSphere(robot.transform.position, robot.SightRange, robot.layerMask);

        float shortestDistance = Mathf.Infinity;
        Collider nearestEnemy = null;

        foreach (Collider enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(robot.transform.position, enemy.transform.position);
            if (distanceToEnemy <= shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= robot.SightRange)
        {
            robot.target = nearestEnemy.transform;

        }
        else
        {
            robot.target = null;
        }
    }

    public void SwitchRangeMode()
    {
        if (robot.target == null)
            return;

        float distance = (robot.target.position - robot.transform.position).sqrMagnitude;

        if (distance >= robot.SightRange * robot.SightRange && !robot.rangedMode && !robot.attacking && robot.isChasing)
        {
            robot.isChasing = false;
            robot.rangedMode = true;
            robot.ChangeState(Robot_Base.Robot_State.READY);
        }
    }

}
