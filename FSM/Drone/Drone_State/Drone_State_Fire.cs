
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 드론의 총알 발사 스테이트
/// </summary>
public class Drone_State_Fire : Base_Interface<Drone>
{
    private bool isTarget = false;
    private float currentDist = 0;      //현재 거리
    private float closetDist = 100f;    //가까운 거리
    private float targetDist = 100f;   //타겟 거리
    private int closeDistIndex = 0;    //가장 가까운 인덱스
    private int targetIndex = -1;      //타겟팅 할 인덱스
    private LayerMask layerMask;

   
    public void OnEnter(Drone drone)
    {
        drone.sphereCollider.radius = drone.range;
        drone.isFire = true;
        drone.droneani.SetTrigger("Fire");
        drone.StartCoroutine(Active_MuzzleEffect(drone));
        drone.StartCoroutine(FireBullet(drone));
    }

    public void OnUpdate(Drone drone)
    {
        //지속적으로 가장 가까운 타겟을 확인한다.
        SetTarget(drone);

        //타겟이 있다면 드론은 타겟을 바라본다.
        if (isTarget)
        {
            drone.transform.LookAt(new Vector3(drone.targetList[targetIndex].transform.position.x,
                                   drone.transform.position.y, drone.targetList[targetIndex].transform.position.z));
        }
    }

    // 몬스터 리스트에 든 표적중 제일 가까운 적을 우선 사격한다.
    void SetTarget(Drone drone)
    {
        // 표적이 없다면 해당 값들을 0혹은 -1로 해준다.
        if (drone.targetList.Count != 0)
        {
            currentDist = 0f;
            closeDistIndex = 0;
            targetIndex = -1;

            //몬스터 리스트를 순회하며 RayCast로 거리를 비교 후 가장 가까운 적을 타겟으로 설정한다.
            for (int i = 0; i < drone.targetList.Count; i++)
            {
                currentDist = Vector3.Distance(drone.transform.position, drone.targetList[i].transform.position);

                RaycastHit hit;
                bool isHit = Physics.Raycast(drone.transform.position, drone.targetList[i].transform.position - drone.transform.position,
                                             out hit, 20f, layerMask);

                if (isHit && hit.transform.CompareTag("Monster"))
                {
                    if (targetDist >= currentDist)
                    {
                        targetIndex = i;
                        targetDist = currentDist;
                    }
                }
                // 가까운적을 우선하여 설정한다.
                if (closetDist >= currentDist)
                {
                    closeDistIndex = i;
                    closetDist = currentDist;
                }
            }

            if (targetIndex == -1)
            {
                targetIndex = closeDistIndex;
            }
            closetDist = 100f;
            targetDist = 100f;
            isTarget = true;
        }
    }

    public void OnFixedUpdate(Drone drone)
    {
      
    }

    public void OnExit(Drone drone)
    {
        drone.targetList.Clear();     
        isTarget = false;
        drone.sphereCollider.radius = 1;
       
    }

    // 발사 이펙트를 켜주고 일정 시간후에 꺼줌
    IEnumerator Active_MuzzleEffect(Drone drone)
    {
        GameObject Blast = ObjectPoolingManager.Instance.GetObject("Drone_Blast", drone.blast_EftPos);
        GameObject Muzzle = ObjectPoolingManager.Instance.GetObject("Drone_Muzzle", drone.muzzle_EftPos);
        yield return StaticCoroutine.Wait(drone.shooting_Time);
        ObjectPoolingManager.Instance.ReturnObject("Drone_Blast", Blast);
        ObjectPoolingManager.Instance.ReturnObject("Drone_Muzzle", Muzzle);
        drone.isFire = false;
        drone.ChangeState(Drone.dState.Idle);
    }

    // 일정시간동안 총알을 오브젝트풀링에서 꺼냄
    IEnumerator FireBullet(Drone drone)
    {
        GameObject Bullet;
        while (drone.isFire)
        {
            yield return StaticCoroutine.Wait(drone.atkSpeed);
            Bullet=ObjectPoolingManager.Instance.GetObject_Noparent("Drone_Bullet", drone.blast_EftPos);
            drone.StartCoroutine(ReturnBullet(Bullet));
        }
    }

    // 일정시간 뒤 총알을 오브젝트풀링으로 반환
    IEnumerator ReturnBullet(GameObject Bullet)
    {
        yield return StaticCoroutine.Wait(1.0f);
        ObjectPoolingManager.Instance.ReturnObject("Drone_Bullet", Bullet);
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
