
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����� �Ѿ� �߻� ������Ʈ
/// </summary>
public class Drone_State_Fire : Interface_Base<Drone>
{
    private bool isTarget = false;
    private float currentDist = 0;      //���� �Ÿ�
    private float closetDist = 100f;    //����� �Ÿ�
    private float targetDist = 100f;   //Ÿ�� �Ÿ�
    private int closeDistIndex = 0;    //���� ����� �ε���
    private int targetIndex = -1;      //Ÿ���� �� �ε���
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
        //���������� ���� ����� Ÿ���� Ȯ���Ѵ�.
        SetTarget(drone);

        //Ÿ���� �ִٸ� ����� Ÿ���� �ٶ󺻴�.
        if (isTarget)
        {
            drone.transform.LookAt(new Vector3(drone.targetList[targetIndex].transform.position.x,
                                   drone.transform.position.y, drone.targetList[targetIndex].transform.position.z));
        }
    }

    // ���� ����Ʈ�� �� ǥ���� ���� ����� ���� �켱 ����Ѵ�.
    void SetTarget(Drone drone)
    {
        // ǥ���� ���ٸ� �ش� ������ 0Ȥ�� -1�� ���ش�.
        if (drone.targetList.Count != 0)
        {
            currentDist = 0f;
            closeDistIndex = 0;
            targetIndex = -1;

            //���� ����Ʈ�� ��ȸ�ϸ� RayCast�� �Ÿ��� �� �� ���� ����� ���� Ÿ������ �����Ѵ�.
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
                // ��������� �켱�Ͽ� �����Ѵ�.
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

    // �߻� ����Ʈ�� ���ְ� ���� �ð��Ŀ� ����
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

    // �����ð����� �Ѿ��� ������ƮǮ������ ����
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

    // �����ð� �� �Ѿ��� ������ƮǮ������ ��ȯ
    IEnumerator ReturnBullet(GameObject Bullet)
    {
        yield return StaticCoroutine.Wait(1.0f);
        ObjectPoolingManager.Instance.ReturnObject("Drone_Bullet", Bullet);
    }
}
