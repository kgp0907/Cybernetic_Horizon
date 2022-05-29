using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public enum dState
    {
        Fire,
        Heal,
        Idle
    }
    public SphereCollider sphereCollider;
    public float turnSpeed = 15f;
    public float range = 1;
    public float atkSpeed = 0.5f;
    public float shooting_Time = 10f;
    public bool isFire = false;
    
    public Transform Player;
    public GameObject blast_EftPos;
    public GameObject muzzle_EftPos;

    public Vector3 offset_Pos;
    public Quaternion offset_Rot;

    public Animator droneani;

    public List<GameObject> targetList = new List<GameObject>();
    private Fsm_Base<Drone> drone_fsm;
  
    private Dictionary<dState, Base_Interface<Drone>> d_states = new Dictionary<dState, Base_Interface<Drone>>();

    void Start()
    {
        d_states.Add(dState.Fire, new Drone_State_Fire());
        d_states.Add(dState.Heal, new Drone_State_Heal());
        d_states.Add(dState.Idle, new Drone_State_Idle());
        drone_fsm = new Fsm_Base<Drone>(this, d_states[dState.Idle]);
    }

    public void ChangeState(dState state)
    {
        drone_fsm.SetState(d_states[state]);
    }

    void Update()
    {
        drone_fsm.OnUpdate();
        transform.position = Vector3.Lerp(transform.position, Player.position - offset_Pos, Time.deltaTime);

        if (!isFire)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Player.rotation * offset_Rot, Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Robot"))
        {
            targetList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Robot"))
        {
            targetList.Remove(other.gameObject);
        }
    }
}