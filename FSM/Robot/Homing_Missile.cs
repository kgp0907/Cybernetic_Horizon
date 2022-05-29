using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing_Missile : MonoBehaviour
{
    public float turnSpeed = 6;
    public float speed = 15f;
    public float maximumSpeed = 40f;
    [SerializeField] Transform target;
    public LayerMask checkLayers;
    public float SightRange = 30f;
    Rigidbody m_rigid = null;
    private string updateTarget = "UpdateTarget";

    void Start()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
        m_rigid = GetComponent<Rigidbody>(); 
        StartCoroutine(DestroyMissile());
    }

    private void Update()
    {
        InvokeRepeating(updateTarget, 0f, 1f);

        if (maximumSpeed <= speed)
            maximumSpeed += speed * Time.deltaTime;


        transform.position += transform.forward * maximumSpeed * Time.deltaTime;

        if (target == null)
            return;
        Vector3 t_dir = (target.position - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, t_dir, 0.25f); //¿ø·¡ Ã³
    
    }


    IEnumerator DestroyMissile()
    {

        yield return StaticCoroutine.Wait(5f);
        GameObject MissileExplosionEffect= ObjectPoolingManager.Instance.GetObject_Noparent("Missle_Explosion", gameObject);
        StartCoroutine(MissileExplosion());  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           StartCoroutine(MissileExplosion());
        }
    }

    IEnumerator MissileExplosion()
    {
        GameObject MissileExplosionEffect = ObjectPoolingManager.Instance.GetObject_Noparent("Missle_Explosion", gameObject);
        yield return StaticCoroutine.Wait(0.1f);
        ObjectPoolingManager.Instance.ReturnObject("Missile", gameObject);
    }

    private void UpdateTarget()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, SightRange, checkLayers);

        float shortestDistance = Mathf.Infinity;
        Collider nearestEnemy = null;

        foreach (Collider enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= SightRange)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }
}