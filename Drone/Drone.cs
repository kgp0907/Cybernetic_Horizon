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

    public float fireRate = 0.5f;
    public Transform Player;
    //public Transform[] target;
    public Vector3 offset;
    public Quaternion offset2;
    public GameObject Blast_Pos;
    public GameObject Muzzle_Pos;
    public float FireTime = 10f;
    //private bool HealSkill = true;

    //private float AttackCoolTime;

    //public GameObject Child;

    //public static int Phase = 0;
    //public ParticleSystem muzzle;
    //public GameObject Heal;
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

    // Update is called once per frame
    private void FixedUpdate()
    {
        d_sm.OnFixedUpdate();
       // transform.rotation = Player.rotation;
        transform.position = Vector3.Lerp(transform.position, Player.position - offset, Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Player.rotation * offset2, Time.deltaTime);
    }

    public void ChangeState(dState state)
    {
        d_sm.SetState(d_states[state]);
    }

    void Update()
    {
        d_sm.OnUpdate();
        // Vector3 direction = new Vector3(target[0].position.x, transform.position.y, target[0].position.z) - transform.position;
        //  Quaternion t_lookRotation = Quaternion.LookRotation(target[0].position);

        //  Vector3 nol = (target[Phase].position - transform.position).normalized;

        //  AttackCoolTime += Time.deltaTime;
    }
}
    //    else if (!CameraFocus.Lockon)
    //    {
    //        droneani.SetBool("Fire", false);
    //    }

    //    if (Input.GetKeyDown(KeyCode.H) && HealSkill)
    //    {
    //        StartCoroutine(HealHP());
    //        StartCoroutine(CoolTime(30f));
    //    }

    //}



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
    //{
    //    yield return new WaitForSeconds(1f);
    //    droneani.SetBool("Fire", false);
    //    droneani.SetTrigger("hit");
    //    Heal.SetActive(true);
    //    Player_HP_Stamina.pHp_Current += 30;
    //    yield return new WaitForSeconds(2f);
    //    Heal.SetActive(false);
    //}


