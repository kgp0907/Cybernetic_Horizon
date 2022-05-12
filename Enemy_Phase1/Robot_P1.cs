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


        ATTACK_TAKEDOWN,
        ATTACK_CLAP,
    
        PUNCH,
        SMASH,
       BOMB,
       HOMING_MISSILE,
       CLAW_ATTACK,
       EARTHQUAKE
    }
    public enum RobotP2_State
    {
        PUNCH
    }
    public bool phase2 = false;
    public bool phase3 = false;
    public GameObject Phase2;
    public GameObject Phase3;
    public GameObject RobotSpawner;
    public Robot_P1_Pattern robotP1_Pattern;
    public float RotationSpeed = 1f;
    public float SightRange = 30f;
    public float ActReadyTime = 2f;
    public float actReadyTime = 4f;
    public SphereCollider P1_Colider;
    public bool dead = false;
    public Animator Phase1_Animator;
    public Animator Phase2_Animator;
    public NavMeshAgent navMeshAgent;
    public float AttackRange = 10f;
    public bool Attacking = false;
    public Transform target;
    public Robot_AI robotAi;
    public string p1_id;
    public bool AnimationName => Phase1_Animator.GetCurrentAnimatorStateInfo(0).IsName(p1_id);
    public float AnimationProgress => Phase1_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    public Dictionary<RobotP1_State, Robot_State<Robot_P1>> r_states = new Dictionary<RobotP1_State, Robot_State<Robot_P1>>();
  //  public Dictionary<RobotP2_State, Robot_State2<Robot_P1>> r_states2 = new Dictionary<RobotP2_State, Robot_State2<Robot_P1>>();
    public rStateMachine<Robot_P1> r_sm;
    public rStateMachine<Robot_P1> curstate;
    // Start is called before the first frame update
    void Awake()
    {
        r_states.Add(RobotP1_State.CHASE, new RobotP1_State_Chase());
        r_states.Add(RobotP1_State.READY, new RobotP1_State_Ready());
        r_states.Add(RobotP1_State.ATTACK_TAKEDOWN, new RobotP1_State_Takedown());
        r_states.Add(RobotP1_State.ATTACK_CLAP, new RobotP1_State_Clap());
        r_states.Add(RobotP1_State.BORN, new RobotP1_State_Born());
        r_states.Add(RobotP1_State.DEAD, new RobotP1_State_Dead());
        r_states.Add(RobotP1_State.SMASH, new RobotP2_State_Smash());
        r_states.Add(RobotP1_State.PUNCH, new RobotP2_State_Punch());
        r_states.Add(RobotP1_State.BOMB, new RobotP3_State_Bomb());
        r_states.Add(RobotP1_State.EARTHQUAKE, new RobotP3_State_EarthQuake());
        r_states.Add(RobotP1_State.HOMING_MISSILE, new RobotP3_State_Missile());
        r_states.Add(RobotP1_State.CLAW_ATTACK, new RobotP3_State_Claw());
        //  r_states.Add(RobotP1_State.PUNCH, new RobotP2_State_Punch());
        //  r_states.Add(RobotP1_State.PUNCH, new RobotP2_State_Punch());
        r_sm = new rStateMachine<Robot_P1>(this, null);
    }

    // Update is called once per frame
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
        if (!phase2)
            Phase1_Animator.SetBool("Walk", true);
        else
        Phase2_Animator.SetBool("Walk", true);
        navMeshAgent.isStopped = false;
    }
    public void StopMove()
    {
        navMeshAgent.velocity = Vector3.zero;
        if(!phase2)
        Phase1_Animator.SetBool("Walk", false);
        else
        Phase2_Animator.SetBool("Walk", false);
        navMeshAgent.isStopped = true;
    }
}
