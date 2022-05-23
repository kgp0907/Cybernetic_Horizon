using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackColision : MonoBehaviour
{
    Player player;
    private string tagname="Robot";
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagname))
        {
            CinemachineImpulse.Instance.CameraShake(3f);
            other.gameObject.GetComponent<Enemy_HP>()?.TakeDamage(player.playerDamage);
        }
    }



    //public void OnCollisionEnter(Collision co)
    //{
    //    Debug.Log("치고있니");
    //    ContactPoint contact = co.contacts[0];
    //    Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
    //    Vector3 pos = contact.point;

    //    GameObject BulletHit = ObjectPoolingManager.Instance.GetObject_Trans("Player_HitEffect", pos, rot);
    //    StartCoroutine(HitEffectReturn(BulletHit));

    //    if (co.gameObject.CompareTag("Robot"))
    //    {
    //        CinemachineImpulse.Instance.CameraShake(3f);
    //        co.gameObject.GetComponent<Enemy_HP>()?.TakeDamage(5);
    //    }
    //}

}
