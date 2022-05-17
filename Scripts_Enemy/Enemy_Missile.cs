using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Missile : MonoBehaviour
{
    public GameObject MissilePrefab;
    public GameObject ExplosionEffect;
    public GameObject RocketTrail;
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
        gameObject.GetComponent<BoxCollider>().enabled = true;
        m_rigid = GetComponent<Rigidbody>();
        StartCoroutine(LaunchDelay());
        StartCoroutine(DestroyMissile());
    }

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
        transform.forward = Vector3.Lerp(transform.forward, t_dir, 0.25f); //¿ø·¡ Ã³
        //Vector3 dir = target.position - transform.position;
        //Quaternion lookRotation = Quaternion.LookRotation(dir);
        ////Vector3 rotation = lookRotation.eulerAngles;

        //Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnspeed).eulerAngles;
        //partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void UpdateTarget()
    {

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

 

    IEnumerator DestroyMissile()
    {
        yield return new WaitForSeconds(7f);
        GameObject MissileExplosion= ObjectPoolingManager.Instance.GetObject_Noparent("Missileex", gameObject);
        StartCoroutine(ReturnMissile(MissileExplosion));  
    }

    IEnumerator LaunchDelay()
    {
        yield return new WaitUntil(() => m_rigid.velocity.y < 0.25f);
        yield return new WaitForSeconds(0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           StartCoroutine(StartExplosion());
        }
    }

    IEnumerator ReturnMissile(GameObject MissileExplosion)
    {
        RocketTrail.SetActive(false);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        MissilePrefab.SetActive(false);
        yield return new WaitForSeconds(1f);
        ObjectPoolingManager.Instance.ReturnObject("Missileex", MissileExplosion);
        yield return new WaitForSeconds(0.5f);
        ObjectPoolingManager.Instance.ReturnObject("Missile", gameObject);
 
    }
    IEnumerator StartExplosion()
    {
        yield return null;
        GameObject MissileExplosion = ObjectPoolingManager.Instance.GetObject_Noparent("Missileex", gameObject);
        StartCoroutine(ReturnMissile(MissileExplosion));
    }
}