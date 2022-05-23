using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class State_Fire : DState<Drone>
{
    public bool getATarget = false;
    float currentDist = 0;      //현재 거리
    float closetDist = 100f;    //가까운 거리
    float TargetDist = 100f;   //타겟 거리
    int closeDistIndex = 0;    //가장 가까운 인덱스
    int TargetIndex = -1;      //타겟팅 할 인덱스
    public LayerMask layerMask;
    private Transform target;
    private string enemyTag = "Robot";
    private bool Fire = false;
    public void OnEnter(Drone drone)
    {
     
        drone.sphereCollider.radius = drone.range;
        drone.isFire = true;
        drone.StartCoroutine(FireCoroutine(drone));
       drone.StartCoroutine(FireBullet(drone));
    }

    public void OnUpdate(Drone drone)
    {
        SetTarget(drone);
        if (getATarget)
        {
            drone.transform.LookAt(new Vector3(drone.MonsterList[TargetIndex].transform.position.x, drone.transform.position.y, drone.MonsterList[TargetIndex].transform.position.z));
        }
    }
    void SetTarget(Drone drone)
    {
        if (drone.MonsterList.Count != 0)
        {
            currentDist = 0f;
            closeDistIndex = 0;
            TargetIndex = -1;

            for (int i = 0; i < drone.MonsterList.Count; i++)
            {
                currentDist = Vector3.Distance(drone.transform.position, drone.MonsterList[i].transform.position);

                RaycastHit hit;
                bool isHit = Physics.Raycast(drone.transform.position, drone.MonsterList[i].transform.position - drone.transform.position,
                                            out hit, 20f, layerMask);

                if (isHit && hit.transform.CompareTag("Monster"))
                {
                    if (TargetDist >= currentDist)
                    {
                        TargetIndex = i;
                        TargetDist = currentDist;
                    }
                }

                if (closetDist >= currentDist)
                {
                    closeDistIndex = i;
                    closetDist = currentDist;
                }
            }

            if (TargetIndex == -1)
            {
                TargetIndex = closeDistIndex;
            }
            closetDist = 100f;
            TargetDist = 100f;
            getATarget = true;
        }
    }
    //void UpdateTarget(Drone drone)
    //{
    //    GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
    //    float shortestDistance = Mathf.Infinity;
    //    GameObject nearestEnemy = null;

    //    foreach(GameObject enemy in enemies)
    //    {
    //        float distanceToEnemy = Vector3.Distance(drone.transform.position, enemy.transform.position);
    //        if(distanceToEnemy <= shortestDistance)
    //        {
    //            shortestDistance = distanceToEnemy;
    //            nearestEnemy = enemy;
    //        }
    //    }

    //    if(nearestEnemy != null&&shortestDistance<=drone.range)
    //    {
    //       target = nearestEnemy.transform;
    //    }
    //    else
    //    {
    //       target = null;
    //    }

    //}

    public void OnFixedUpdate(Drone drone)
    {
      
    }

    public void OnExit(Drone drone)
    {
        drone.isFire = false;
        drone.MonsterList.Clear();
        
        getATarget = false;
        drone.sphereCollider.radius = 1;
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


    //public void EnemyCheck(Drone drone)
    //{
    //    Collider[] colls = Physics.OverlapSphere(drone.transform.position, 30f);

    //    foreach (Collider playerobj in colls)
    //    {
    //        if (playerobj.gameObject.CompareTag("Robot"))
    //        {
    //            drone.MonsterList.Add(playerobj.gameObject);
    //        }
    //    }

    //    if (drone.MonsterList.Count > 0)
    //    {
    //        drone.MonsterList.Clear();
    //    }
    //}

    //UpdateTarget(drone);
    //if (target == null)
    //    return;

    //Vector3 dir = target.position - drone.transform.position;
    //Quaternion lookRotation = Quaternion.LookRotation(dir);
    //Vector3 rotation = lookRotation.eulerAngles;
    ////vector3 rotation=quternion.lerp(partorotate.rotation,lookrotation,time deltatime*drone.turnspeed).eulerangle
    //drone.partToRotate.rotation = Quaternion.Euler(0f,rotation.y,0f);
}
