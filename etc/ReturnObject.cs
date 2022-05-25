using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnObject : MonoBehaviour
{
    public string tagName;
    public float DurationTime;

    public void OnEnable()
    {
        StartCoroutine(ReturnCoroutine());   
    }

    IEnumerator ReturnCoroutine()
    {
        yield return YieldInstructionCache.WaitForSeconds(DurationTime);
        ObjectPoolingManager.Instance.ReturnObject(tagName, gameObject);
    }
}
