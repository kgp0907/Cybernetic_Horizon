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
}
