using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Fire : DState<Drone>
{
    private bool Fire = false;

    public void OnEnter(Drone drone)
    {
        drone.StartCoroutine(FireCoroutine(drone));
       drone.StartCoroutine(FireBullet(drone));
    }

    public void OnUpdate(Drone drone)
    {
  
    }

    public void OnFixedUpdate(Drone drone)
    {

    }

    public void OnExit(Drone drone)
    {
          

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
