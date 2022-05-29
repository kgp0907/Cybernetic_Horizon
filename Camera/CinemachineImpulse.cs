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

    // �Ű������� ���� �����Ҽ��� ������ ����ũ,
    // ��ũ��Ʈ ���� ī�޶� impulseListener �ʿ�
    public void CameraShake(float shake)
    {
        impulse.GenerateImpulse(shake);
    }

  
}
