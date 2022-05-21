using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance2 { get; private set; }
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;

    private void Awake()
    {
          Instance2 = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera2(float intensity,float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
       
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
      //  StartCoroutine(ShakeCameraCoroutine());
    }


  

    IEnumerator ShakeCameraCoroutine()
    {
        Debug.Log(shakeTimer);
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                  cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
            yield return null;
        }
      
    }
}
