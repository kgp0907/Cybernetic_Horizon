using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsenceEvent : MonoBehaviour
{
    public TimeLineManager timeLineManager;

    private void Start()
    {
        timeLineManager.GetComponent<TimeLineManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timeLineManager.PlayFromTimeLine();
        }
    }
}
