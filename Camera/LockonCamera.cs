using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockonCamera : MonoBehaviour
{
    public Player player;
    private string enemyTag = "Robot";
    Cinemachine.CinemachineVirtualCamera c_VirtualCamera;
    [SerializeField] Transform target;

    public float SightRange = 30f;

    private void Awake()
    {
        c_VirtualCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    private void Update()
    {
        
        UpdateTarget();
        if (target == null)
            return;

        Vector3 dir = target.position - player.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
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
