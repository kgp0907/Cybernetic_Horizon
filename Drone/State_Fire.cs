using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Fire : DState<Drone>
{
   
    public float bullettime = 0;
    public void OnEnter(Drone drone)
    {
        drone.StartCoroutine(FireCoroutine(drone));
       drone.StartCoroutine(FireTurn(drone));
    }

    public void OnUpdate(Drone drone)
    {

      // drone.StartCoroutine(FireTurn(drone));
   

    }

    public void OnFixedUpdate(Drone drone)
    {

    }

    public void OnExit(Drone drone)
    {
        drone.droneani.SetTrigger("Fire");
       
    }

    IEnumerator FireCoroutine(Drone drone)
    {
        drone.droneani.SetTrigger("Fire");
        GameObject Blast = ObjectPoolingManager.Instance.GetObject("Drone_Blast", drone.Blast_Pos);
        GameObject Muzzle = ObjectPoolingManager.Instance.GetObject("Drone_Muzzle", drone.Muzzle_Pos);
        yield return drone.FireTime;
        ObjectPoolingManager.Instance.ReturnObject("Drone_Blast", Blast);
        ObjectPoolingManager.Instance.ReturnObject("Drone_Muzzle", Muzzle);
        drone.ChangeState(Drone.dState.Idle);
    }

    IEnumerator FireTurn(Drone drone)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            GameObject Bullet = ObjectPoolingManager.Instance.GetObject_Noparent("Drone_Bullet", drone.Blast_Pos);
            Fireend(drone, Bullet);
        }
      

    }
   IEnumerator Fireend(Drone drone,GameObject bullet)
    {
        yield return new WaitForSeconds(0.2f);
        ObjectPoolingManager.Instance.ReturnObject("Drone_Bullet", bullet);
    }
}
