using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MissileScript : MonoBehaviour
{


    public GameObject MissilePrefab;
    public List<GameObject> spawnPosition;
    public GameObject ExplosionEffect;
    public GameObject target;
    public ParticleSystem[] SmokeEffect;
    public float speed = 4f;



    private GameObject Missile;
    // Start is called before the first frame update
    void MissileHoming()
    {
        
     
        for (int i = 0; i < 4; i++)
        {
            SmokeEffect[i].Play();
            Missile = Instantiate(MissilePrefab, spawnPosition[i].transform.position, Quaternion.identity);
            //Missile.transform.LookAt(target.transform);
            MissilePrefab.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;

           //  StartCoroutine(sendHoming(Missile));

        }
       

    }

  

    public IEnumerator sendHoming(GameObject missile)
    {
      
        while (Vector3.Distance(target.transform.position, missile.transform.position) > 0.3f)
        {
            
          //  missile.transform.position += (target.transform.position - missile.transform.position).normalized * speed * Time.deltaTime;
         //   missile.transform.LookAt(target.transform);
           yield return null;
     
            
        }
      
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        Instantiate(ExplosionEffect, MissilePrefab.transform.position, MissilePrefab.transform.rotation);
    //        Destroy(MissilePrefab);
    //    }

    //}

    

}
