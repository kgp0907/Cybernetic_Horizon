using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Robot_Base : MonoBehaviour
{
    public enum RobotP1_State
    {
       DEAD,
       BORN,
       CHASE,
       READY,
       TRANFORM,

       P1_ATTACK_TAKEDOWN,
       P1_ATTACK_CLAP,
    
       P2_ATTACK_PUNCH,
       P2_ATTACK_SMASH,

       P3_ATTACK_BOMB,
       P3_ATTACK_HOMING_MISSILE,
       P3_ATTACK_CLAW_ATTACK,
       P3_ATTACK_EARTHQUAKE
    }
    //Stat
    public string Animation_id;
    public float attackRange = 10f;
    public float SightRange = 30f;
    public float RotationSpeed = 1f;
    public float ActReadyTime = 2f;
    public float actReadyTime = 4f;

    public GameObject next_TransformObject;
    public GameObject effectPos_Shockwave;
    public bool attacking = false;
    public bool rangedMode = false;
    public bool isPhase1;
    public bool isPhase2;
    public bool isPhase3;
    public Transform target;
    private string walk = "Walk";

    //script
    public Robot_P1 RobotP1;
    public Robot_P2 RobotP2;
    public Robot_P3 RobotP3;
    public Enemy_Pattern robotP1_Pattern;
    public Robot_AI robotAi;
    public Animator robot_Animator;
    public NavMeshAgent navMeshAgent;

    public bool AnimationName => robot_Animator.GetCurrentAnimatorStateInfo(0).IsName(Animation_id);
    public float AnimationProgress => robot_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    public Dictionary<RobotP1_State, Base_Interface<Robot_Base>> robot_States = new Dictionary<RobotP1_State, Base_Interface<Robot_Base>>();
    public Fsm_Base<Robot_Base> robot_StateMachine;
    public Fsm_Base<Robot_Base> curstate;
    void Awake()
    {
        robot_States.Add(RobotP1_State.CHASE, new Robot_State_Chase());
        robot_States.Add(RobotP1_State.READY, new Robot_State_Ready());
        robot_States.Add(RobotP1_State.P1_ATTACK_TAKEDOWN, new RobotP1_State_Takedown());
        robot_States.Add(RobotP1_State.P1_ATTACK_CLAP, new RobotP1_State_ClapAtk());
        robot_States.Add(RobotP1_State.BORN, new Robot_State_Born());
        robot_States.Add(RobotP1_State.DEAD, new Robot_State_Dead());
        robot_States.Add(RobotP1_State.TRANFORM, new Robot_State_Transform());
        robot_States.Add(RobotP1_State.P2_ATTACK_SMASH, new RobotP2_State_Smash());
        robot_States.Add(RobotP1_State.P2_ATTACK_PUNCH, new RobotP2_State_Punch());
        robot_States.Add(RobotP1_State.P3_ATTACK_BOMB, new RobotP3_State_Bomb());
        robot_States.Add(RobotP1_State.P3_ATTACK_EARTHQUAKE, new RobotP3_State_EarthQuake());
        robot_States.Add(RobotP1_State.P3_ATTACK_HOMING_MISSILE, new RobotP3_State_Missile());
        robot_States.Add(RobotP1_State.P3_ATTACK_CLAW_ATTACK, new RobotP3_State_ClawAtk());
        robot_StateMachine = new Fsm_Base<Robot_Base>(this, null);
    }

    void Update()
    {
        robot_StateMachine.OnUpdate();
    }

    private void FixedUpdate()
    {
        robot_StateMachine.OnFixedUpdate();
    }

    public bool CurState(RobotP1_State state)
    {
        return robot_StateMachine.CurState == robot_States[state];
    }

    public void ChangeState(RobotP1_State state)
    {
        robot_StateMachine.SetState(robot_States[state]);
    }

    public void StartMove()
    {
        robot_Animator.SetBool(walk, true);
        navMeshAgent.isStopped = false;
    }
    public void StopMove()
    {
        navMeshAgent.velocity = Vector3.zero;
        robot_Animator.SetBool(walk, false);
        navMeshAgent.isStopped = true;
    }

    public void Shskecamera()
    {
        CinemachineImpulse.Instance.CameraShake(3f);
    }
}