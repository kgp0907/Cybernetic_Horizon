using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnObject : MonoBehaviour
{
    public string tagName;
    public float durationTime;

    public void OnEnable()
    {
        StartCoroutine(ReturnCoroutine());   
    }

    IEnumerator ReturnCoroutine()
    {
        yield return StaticCoroutine.Wait(durationTime);
        ObjectPoolingManager.Instance.ReturnObject(tagName, gameObject);
    }
}
