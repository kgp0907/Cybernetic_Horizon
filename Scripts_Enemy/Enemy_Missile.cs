using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Missile : MonoBehaviour
{
    public float turnspeed = 6;
    public Transform partToRotate;
    private Transform target;
    Rigidbody m_rigid = null;
    public float range = 40;
    private string targetTag = "Player";
    [SerializeField] float m_speed = 15f;
    public float m_currentSpeed = 40f;
    [SerializeField] LayerMask m_layerMAsk = 0;
    [SerializeField] ParticleSystem m_psEffect = null;

    //public GameObject misiilePrefab;

   // public GameObject ExplosionEffect;


    void Start()
    {

        m_rigid = GetComponent<Rigidbody>();
        StartCoroutine(LaunchDelay());
        StartCoroutine(DestroyMissile());
    }





    // Update is called once per frame
    private void Update()
    {
        //  UpdateTarget();
        
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        if (m_currentSpeed <= m_speed)
            m_currentSpeed += m_speed * Time.deltaTime;


        transform.position += transform.forward * m_currentSpeed * Time.deltaTime;

        if (target == null)
            return;
        Vector3 t_dir = (target.position - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, t_dir, 0.25f); //원래 처
        //Vector3 dir = target.position - transform.position;
        //Quaternion lookRotation = Quaternion.LookRotation(dir);
        ////Vector3 rotation = lookRotation.eulerAngles;

        //Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnspeed).eulerAngles;
        //partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void UpdateTarget()
    {
        Debug.Log(target);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }

    private void FixedUpdate()

    {
        ////점점 속력을 붙여줌
 

        ////미사일은 앞으로 계속이동


        ////방향을 구하고, 회전하게만듬
  
    }

    IEnumerator DestroyMissile()
    {
        yield return new WaitForSeconds(3f);
        ObjectPoolingManager.Instance.ReturnObject("Missile", gameObject);
    }

    IEnumerator LaunchDelay()
    {
        yield return new WaitUntil(() => m_rigid.velocity.y < 0f);
        yield return new WaitForSeconds(0.5f);

       // m_psEffect.Play();


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ObjectPoolingManager.Instance.ReturnObject("Missile", gameObject);
            gameObject.SetActive(false);
        }
    }
}