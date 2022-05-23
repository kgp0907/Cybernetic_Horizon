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
    public float range = 15f;
    public float fireRate = 0.5f;
    public float FireTime = 10f;
    public bool isFire = false;
    
    public Transform Player;
    public GameObject Blast_Pos;
    public GameObject Muzzle_Pos;
    public Vector3 followOffset_Pos;
    public Quaternion followOffset_Rot;

   
    //private bool HealSkill = true;
    public List<GameObject> MonsterList = new List<GameObject>();
    public Animator droneani;
    private dStateMachine<Drone> d_sm;
  
    private Dictionary<dState, DState<Drone>> d_states = new Dictionary<dState, DState<Drone>>();

    [SerializeField] private GameObject bulletPrefab;

    private void Start()
    {
        d_states.Add(dState.Fire, new State_Fire());
        d_states.Add(dState.Heal, new State_Heal());
        d_states.Add(dState.Idle, new State_Idle());
        d_sm = new dStateMachine<Drone>(this, d_states[dState.Idle]);
    }

    private void FixedUpdate()
    {
        d_sm.OnFixedUpdate();
    }

    public void ChangeState(dState state)
    {
        d_sm.SetState(d_states[state]);
    }

    void Update()
    {
        d_sm.OnUpdate();
        transform.position = Vector3.Lerp(transform.position, Player.position - followOffset_Pos, Time.deltaTime);

        if (!isFire)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Player.rotation * followOffset_Rot, Time.deltaTime);
        }
        //if (Input.GetKeyDown(KeyCode.H) && HealSkill)
        //{
        //    StartCoroutine(HealHP());
        //    StartCoroutine(CoolTime(30f));
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Robot"))
        {
            MonsterList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Robot"))
        {
            MonsterList.Remove(other.gameObject);
        }
    }


    //IEnumerator CoolTime(float cool)
    //{
    //    HealSkill = false;
    //    while (cool > 1.0f)
    //    {
    //        cool -= Time.deltaTime;
    //        yield return new WaitForFixedUpdate();
    //    }

    //    if (cool <= 0)
    //    {
    //        HealSkill = true;
    //    }
    //}

    //IEnumerator HealHP()
    //    {
    //        yield return new WaitForSeconds(1f);
    //        droneani.SetBool("Fire", false);
    //        droneani.SetTrigger("hit");
    //        Heal.SetActive(true);
    //        Player_HP_Stamina.pHp_Current += 30;
    //        yield return new WaitForSeconds(2f);
    //        Heal.SetActive(false);
    //    }
}








