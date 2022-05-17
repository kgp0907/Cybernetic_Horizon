using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Robot_P1 : MonoBehaviour
{
    public enum RobotP1_State
    {
       DEAD,
       BORN,
       CHASE,
       READY,

       P1_ATTACK_TAKEDOWN,
       P1_ATTACK_CLAP,
    
       P2_ATTACK_PUNCH,
       P2_ATTACK_SMASH,

       P3_ATTACK_BOMB,
       P3_ATTACK_HOMING_MISSILE,
       P3_ATTACK_CLAW_ATTACK,
       P3_ATTACK_EARTHQUAKE
    }
    public string p1_id;
    public Robot_P2 RobotP2;
    public Robot_P3 RobotP3;
    public float AttackRange = 10f;
    public float SightRange = 30f;
    public float RotationSpeed = 1f;
    public float ActReadyTime = 2f;
    public float actReadyTime = 4f;
  
  
    public bool Attacking = false;
    public bool dead = false;

    public bool RangedMode = false;
    public GameObject Phase2;
    public GameObject Phase3;
    public GameObject RobotSpawner;
    public Robot_P1_Pattern robotP1_Pattern;
  
    public Animator Robot_Animator;
    public NavMeshAgent navMeshAgent;
    public Transform target;
    public Robot_AI robotAi;
    public GameObject ImpactEffectPos;
    public GameObject Colision_P1_RightArm;
    public GameObject Colision_P1_LeftArm;


    public bool AnimationName => Robot_Animator.GetCurrentAnimatorStateInfo(0).IsName(p1_id);
    public float AnimationProgress => Robot_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    public Dictionary<RobotP1_State, Robot_State<Robot_P1>> r_states = new Dictionary<RobotP1_State, Robot_State<Robot_P1>>();
    public rStateMachine<Robot_P1> r_sm;
    public rStateMachine<Robot_P1> curstate;
    void Awake()
    {
        r_states.Add(RobotP1_State.CHASE, new RobotP1_State_Chase());
        r_states.Add(RobotP1_State.READY, new RobotP1_State_Ready());
        r_states.Add(RobotP1_State.P1_ATTACK_TAKEDOWN, new RobotP1_State_Takedown());
        r_states.Add(RobotP1_State.P1_ATTACK_CLAP, new RobotP1_State_Clap());
        r_states.Add(RobotP1_State.BORN, new RobotP1_State_Born());
        r_states.Add(RobotP1_State.DEAD, new RobotP1_State_Dead());
        r_states.Add(RobotP1_State.P2_ATTACK_SMASH, new RobotP2_State_Smash());
        r_states.Add(RobotP1_State.P2_ATTACK_PUNCH, new RobotP2_State_Punch());
        r_states.Add(RobotP1_State.P3_ATTACK_BOMB, new RobotP3_State_Bomb());
        r_states.Add(RobotP1_State.P3_ATTACK_EARTHQUAKE, new RobotP3_State_EarthQuake());
        r_states.Add(RobotP1_State.P3_ATTACK_HOMING_MISSILE, new RobotP3_State_Missile());
        r_states.Add(RobotP1_State.P3_ATTACK_CLAW_ATTACK, new RobotP3_State_Claw());
        r_sm = new rStateMachine<Robot_P1>(this, null);
    }

    void Update()
    {
        r_sm.OnUpdate();
    }

    private void FixedUpdate()
    {
        r_sm.OnFixedUpdate();
    }

    public bool IsState(RobotP1_State state)
    {
        return r_sm.CurState == r_states[state];
    }

    public void ChangeState(RobotP1_State state)
    {
        r_sm.SetState(r_states[state]);
    }

    public void StartMove()
    {
        Robot_Animator.SetBool("Walk", true);
        navMeshAgent.isStopped = false;
    }
    public void StopMove()
    {
        navMeshAgent.velocity = Vector3.zero;
        Robot_Animator.SetBool("Walk", false);
        navMeshAgent.isStopped = true;
    }

    public void Shskecamera()
    {
        ShakeCamera.instance.OnShakeCamera(0.1f, 0.1f);
    }
}
