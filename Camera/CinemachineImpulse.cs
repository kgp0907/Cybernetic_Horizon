using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
public class CinemachineImpulse : SingletonBase<CinemachineImpulse>
{
    CinemachineImpulseSource impulse;
   
    void Start()
    {
        impulse = transform.GetComponent<CinemachineImpulseSource>();
    }

    // �Ű������� ���� �����Ҽ��� ������ ����ũ,
    // ��ũ��Ʈ ���� ī�޶� impulseListener �ʿ�
    public void CameraShake(float shake)
    {
        impulse.GenerateImpulse(shake);
    }
}
