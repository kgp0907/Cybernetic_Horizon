using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Fsm_Base<Drone>
{
    public enum DState
    {
        Fire,
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
    public List<GameObject> targetList = new List<GameObject>();

    private Dictionary<DState, Interface_Base<Drone>> d_states = new Dictionary<DState, Interface_Base<Drone>>();

    void Start()
    {
        d_states.Add(DState.Fire, new Drone_State_Fire());
        d_states.Add(DState.Idle, new Drone_State_Idle());
        First_State(this, d_states[DState.Idle]);
    }

    public void ChangeState(DState state)
    {
        SetState(d_states[state]);
    }

    void Update()
    {
        OnUpdate();
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