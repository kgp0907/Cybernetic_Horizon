using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
public class CinemachineImpulse : MonoBehaviour
{
    public static CinemachineImpulse Instance { get; private set; }
    CinemachineImpulseSource impulse;
   
    void Start()
    {
        Instance = this;
        impulse = transform.GetComponent<CinemachineImpulseSource>();
    }

    // 매개변수의 값이 증가할수록 강력한 쉐이크,
    // 스크립트 부착 카메라에 impulseListener 필요
    public void CameraShake(float shake)
    {
        impulse.GenerateImpulse(shake);
    }

  
}
