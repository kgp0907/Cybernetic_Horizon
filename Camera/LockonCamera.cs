using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockonCamera : MonoBehaviour
{
    private CinemachineSwitcher cinemachineSwitcher;
    public Player player;
    Cinemachine.CinemachineVirtualCamera c_VirtualCamera;
    [SerializeField] Transform target;
    public LayerMask checkLayers;
    public float SightRange = 30f;

    private void Awake()
    {
        c_VirtualCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        cinemachineSwitcher = FindObjectOfType<CinemachineSwitcher>();
    }

    private void Update()
    {
        if (cinemachineSwitcher.isLockOnCamera == true)
        {
            UpdateTarget();
            if (target == null)
                return;

            Vector3 dir = target.position - player.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
        }  
    }

    void UpdateTarget()
    {
        Collider[] enemies = Physics.OverlapSphere(player.transform.position, SightRange, checkLayers);

        float shortestDistance = Mathf.Infinity;
        Collider nearestEnemy = null;

        foreach (Collider enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(player.transform.position, enemy.transform.position);
            if (distanceToEnemy <= shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= SightRange)
        {
            target = nearestEnemy.transform;
            c_VirtualCamera.m_LookAt = target.transform;
        }
        else
        {
            target = null;
        }

    }
}
