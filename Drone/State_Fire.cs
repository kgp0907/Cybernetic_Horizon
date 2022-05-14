using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Fire : DState<Drone>
{
    
    private Transform target;
    private string enemyTag = "Enemy";
    private bool Fire = false;
    public void OnEnter(Drone drone)
    {
        drone.fire = true;
        drone.StartCoroutine(FireCoroutine(drone));
       drone.StartCoroutine(FireBullet(drone));
    }

    public void OnUpdate(Drone drone)
    {
        UpdateTarget(drone);
        if (target == null)
            return;

        Vector3 dir = target.position - drone.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        //vector3 rotation=quternion.lerp(partorotate.rotation,lookrotation,time deltatime*drone.turnspeed).eulerangle
        drone.partToRotate.rotation = Quaternion.Euler(0f,rotation.y,0f);
    }

    void UpdateTarget(Drone drone)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(drone.transform.position, enemy.transform.position);
            if(distanceToEnemy <= shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null&&shortestDistance<=drone.range)
        {
           target = nearestEnemy.transform;
        }
        else
        {
           target = null;
        }

    }

public void OnFixedUpdate(Drone drone)
    {
      
    }

    public void OnExit(Drone drone)
    {
        drone.fire = false;

    }

    IEnumerator FireCoroutine(Drone drone)
    {
      
        Fire = true;
        drone.droneani.SetTrigger("Fire");
        GameObject Blast = ObjectPoolingManager.Instance.GetObject("Drone_Blast", drone.Blast_Pos);
        GameObject Muzzle = ObjectPoolingManager.Instance.GetObject("Drone_Muzzle", drone.Muzzle_Pos);
        yield return new WaitForSeconds(drone.FireTime);
        ObjectPoolingManager.Instance.ReturnObject("Drone_Blast", Blast);
        ObjectPoolingManager.Instance.ReturnObject("Drone_Muzzle", Muzzle);
        Fire = false;
        drone.ChangeState(Drone.dState.Idle);

    }

    IEnumerator FireBullet(Drone drone)
    {
        while (Fire)
        {
            yield return new WaitForSeconds(drone.fireRate);
            GameObject Bullet = ObjectPoolingManager.Instance.GetObject_Noparent("Drone_Bullet", drone.Blast_Pos);
            drone.StartCoroutine(ReturnBullet(Bullet));
        }
    }

    IEnumerator ReturnBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(1f);
        ObjectPoolingManager.Instance.ReturnObject("Drone_Bullet", bullet);
    }

   
}
